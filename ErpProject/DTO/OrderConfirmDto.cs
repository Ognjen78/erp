using ErpProject.Models;

namespace ErpProject.DTO
{
    public class OrderConfirmDto
    {
        public int id_order { get; set; }
        public DateTime order_date { get; set; }
        public decimal total_price { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
