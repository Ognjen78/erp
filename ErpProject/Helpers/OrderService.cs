using ErpProject.Controllers;
using ErpProject.DTO;
using ErpProject.Interface;
using ErpProject.Models;

namespace ErpProject.Helpers
{
    public class OrderService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IOrderRepository orderRepository;

        public OrderService(ApplicationDbContext dbContext, IOrderRepository orderRepository)
        {
            this.dbContext = dbContext;
            this.orderRepository = orderRepository;
        }

        public async Task<Order> CreateOrder(CreateOrderRequest request)
        {
            var order = new Order
            {
                order_date = DateTime.UtcNow,
                id_transaction = Guid.NewGuid(),
                transaction_date = DateTime.UtcNow,
                id_user = request.id_user,
                id_shipping = request.id_shipping,
                OrderItems = new List<OrderItem>()
            };

            decimal itemsPrice = 0;

            foreach (var item in request.Items)
            {
                var product = await dbContext.Products.FindAsync(item.id_product);
                if (product != null)
                {
                    var orderItem = new OrderItem
                    {
                        id_product = item.id_product,
                        quantity = item.quantity,
                        price = product.price * item.quantity
                    };
                    order.OrderItems.Add(orderItem);
                    itemsPrice += orderItem.price;
                }
            }

            var shippingAddress = await dbContext.ShippingAddresses.FindAsync(request.id_shipping);
            decimal shippingPrice = shippingAddress?.shipping_price ?? 0;

            order.items_price = itemsPrice;
            order.total_price = itemsPrice + shippingPrice;
            order.transaction_amount = order.total_price;

            return orderRepository.addOrder(order);
        }
    }
}
