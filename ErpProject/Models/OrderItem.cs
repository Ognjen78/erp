using System.ComponentModel.DataAnnotations;

namespace ErpProject.Models
{
    public class OrderItem
    {
        [Key]
        public int id_order_items { get; set; }
        public int id_order { get; set; }
        public int id_product { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }

    }
}
