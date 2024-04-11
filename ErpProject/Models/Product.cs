using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ErpProject.Models
{
    public class Product
    {

        [Key]
        public int id_product { get; set; }
        public string name { get; set; }
        public string brand { get; set; }
        public string category { get; set; }

        public string description { get; set; }
        public decimal price { get; set; }
        
        public int stock { get; set; }
        
    }
}
