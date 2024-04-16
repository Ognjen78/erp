using ErpProject.Interface;
using ErpProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Helpers;

namespace ErpProject.Controllers
{
    [ApiController]
    [Route("api/sportbasic/users")]
    [EnableCors("AllowOrigin")]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly PasswordHashService passwordHashService;
        
        public UserController(IUserRepository userRepository, PasswordHashService passwordHashService)
        {
            this.userRepository = userRepository;   
            this.passwordHashService = passwordHashService;
        }


        [HttpGet]
        [Authorize(Policy = "RequireAdminRole")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<User>> GetAllUsers()
        {
            var users = userRepository.getAllUsers();
            if ( users  == null || users.Count == 0)
            {
                return NoContent();
            }
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<User> GetUserById(Guid id)
        {
            User user = userRepository.getUserById(id);
            if (user == null)
            {
                return NoContent();
            }
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<User> CreateUser([FromBody]User user)
        {
            try
            {
                bool check = userRepository.uniqueEmail(user.email);
                if(check) 
                {
                    return BadRequest("User with this email already exists");
                }
                var password = passwordHashService.HashPassword(user.password);
                user.password = password.Item1;
                user.salt = password.Item2;

                userRepository.addUser(user);
                return Ok(user);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Insert error");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<User> UpdateUser(User user)
        {
            try
            {
                if (userRepository.getUserById(user.id_user) == null)
                {
                    return NotFound("Enter valid ID");
                }
                else if (userRepository.uniqueEmail(user.email))
                {
                    return BadRequest("User with this email already exists");
                }

                userRepository.updateUser(user);
                return Ok(user);
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
                if(userRepository.getUserById(id) == null)
                {
                    return NotFound("User with ID not found.");
                }
                userRepository.deleteUser(id);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }
    }
}
