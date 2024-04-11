using System.ComponentModel.DataAnnotations;

namespace ErpProject.Models
{
    public class ShippingAddress
    {
        [Key]
        public int id_shipping { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public decimal shipping_price { get; set; }
    }
}
