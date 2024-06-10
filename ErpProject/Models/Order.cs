using System.ComponentModel.DataAnnotations;

namespace ErpProject.Models
{
    public class Order
    {
        [Key]
        public int id_order { get; set; }
        public DateTime order_date { get; set; }
        public decimal items_price { get; set; }
        public decimal total_price { get; set; }
        public Guid id_transaction { get; set; }
        public DateTime transaction_date { get; set; }
        public decimal transaction_amount { get; set; }
        public Guid id_user { get; set; }
        public int id_shipping { get; set; }

        public List<OrderItem> OrderItems { get; set; }

    }
}
