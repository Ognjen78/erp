using System.ComponentModel.DataAnnotations;

namespace ErpProject.Models
{
    public class User
    {
        [Key]
        public Guid id_user { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string surname { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Unesite ispravnu email adresu.")]
        public string email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Sifra mora sadrzati barem 8 karaktera.")]
        public string password { get; set; }
        [Required]
        public string username { get; set; }

        public string salt { get; set; }


    }
}
