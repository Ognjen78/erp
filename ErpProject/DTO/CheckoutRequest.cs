namespace ErpProject.DTO
{
    public class CheckoutRequest
    {
        public List<LineItemDto> items { get; set; }
        public OrderDto orderRequest { get; set; }
    }
}
