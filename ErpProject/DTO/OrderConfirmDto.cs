namespace ErpProject.DTO
{
    public class OrderConfirmDto
    {
        public DateTime order_date { get; set; }
        public decimal total_price { get; set; }

        public DateTime transaction_date { get; set; }
        public decimal transaction_amount { get; set; }
    }
}
