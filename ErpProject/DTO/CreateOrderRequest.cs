namespace ErpProject.DTO
{
    public class CreateOrderRequest
    {
        public Guid id_user { get; set; }
        public int id_shipping { get; set; }

        public decimal price { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
