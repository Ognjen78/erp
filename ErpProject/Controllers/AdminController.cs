using ErpProject.Interface;
using ErpProject.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WebApplication5.Helpers;

namespace ErpProject.Controllers
{
    [ApiController]
    [Route("/api/sportbasic/admins")]
    [EnableCors("AllowOrigin")]
    public class AdminController : Controller
    {
        private readonly IAdminRepository adminRepository;
        private readonly PasswordHashService passwordHashService;
        public AdminController(IAdminRepository adminRepository, PasswordHashService passwordHashService)
        {
            this.passwordHashService = passwordHashService;
            this.adminRepository = adminRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Admin>> GetAllAdmins()
        {
            var admins = adminRepository.getAllAdmins();
            if (admins == null || admins.Count == 0)
            {
                return NoContent();
            }
            return Ok(admins);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<Admin> GetAdminById(Guid id)
        {
            Admin admin = adminRepository.getAdminById(id);
            if (admin == null)
            {
                return NoContent();
            }
            return Ok(admin);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Admin> CreateAdmin([FromBody] Admin admin)
        {
            //try
            //{
                bool check = adminRepository.uniqueEmail(admin.email);
                if (check)
                {
                    return BadRequest("Admin with this email already exists");
                }
                var password = passwordHashService.HashPassword(admin.password);
                admin.password = password.Item1;
                admin.salt = password.Item2;

                Admin ad = adminRepository.addAdmin(admin);
                return Ok(ad);
            //}
           /* catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Insert error");
            } */
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<Admin> UpdateAdmin(Admin admin)
        {
            try
            {
                if (adminRepository.getAdminById(admin.id_admin) == null)
                {
                    return NotFound("Enter valid ID");
                }
                else if (adminRepository.uniqueEmail(admin.email))
                {
                    return BadRequest("User with this email already exists");
                }

                adminRepository.updateAdmin(admin);
                return Ok(admin);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            try
            {
                if (adminRepository.getAdminById(id) == null)
                {
                    return NotFound("User with ID not found.");
                }
                adminRepository.deleteAdmin(id);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }
    }
}
