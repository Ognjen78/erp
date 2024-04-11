using System.ComponentModel.DataAnnotations;

namespace ErpProject.Models
{
    public class User
    {
        [Key]
        public Guid id_user { get; set; }
        public string name { get; set; }
        public string surname { get; set; }

        public string email { get; set; }
        public string password { get; set; }
        public string username { get; set; }

        public string salt { get; set; }


    }
}
