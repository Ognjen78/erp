namespace ErpProject.DTO
{
    public class OrderItemConfirmDto
    {
        public int id_order { get; set; }
        public int id_product { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
    }
}
