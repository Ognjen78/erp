using System.ComponentModel.DataAnnotations;

namespace ErpProject.Models
{
    public class Admin
    {
        [Key]
        public Guid id_admin { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
    }
}
