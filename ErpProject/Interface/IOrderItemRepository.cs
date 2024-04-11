using ErpProject.Models;

namespace ErpProject.Interface
{
    public interface IOrderItemRepository
    {
        List<OrderItem> getAllOrderItems();
        OrderItem getOrderItemById(int id);
        OrderItem addOrderItem(OrderItem orderItem);
        OrderItem updateOrderItem(OrderItem orderItem);
        void deleteOrderItem(int id);
    }
}
