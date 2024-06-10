using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("id_order")]
        public Order Order { get; set; }

    }
}
