namespace ErpProject.DTO
{
    public class OrderDto
    {
        public Guid id_user { get; set; }
        public int id_shipping { get; set; }
        public decimal total_price { get; set; }
        public List<LineItemDto> items { get; set; }
    }
}
