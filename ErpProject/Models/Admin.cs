﻿using System.ComponentModel.DataAnnotations;

namespace ErpProject.Models
{
    public class Admin
    {
        [Key]
        public Guid id_admin { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Unesite ispravnu email adresu.")]
        public string email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Sifra mora sadrzati barem 8 karaktera.")]
        public string password { get; set; }
        public string salt { get; set; }
    }
}
