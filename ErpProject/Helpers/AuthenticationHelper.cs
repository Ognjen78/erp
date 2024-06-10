using ErpProject.DTO;
using ErpProject.Interface;
using ErpProject.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;


namespace WebApplication5.Helpers
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        private readonly IConfiguration configuration;
        private readonly IUserRepository userRepository;
        private readonly IAdminRepository adminRepository;

        public AuthenticationHelper(IConfiguration configuration, IUserRepository userRepository, IAdminRepository adminRepository)
        {
            this.configuration = configuration;
            this.userRepository = userRepository;
            this.adminRepository = adminRepository;
        }

        public bool AuthenticatePrincipal(UserLoginDto userLogin)
        {
            if (userRepository.UserWithCredentialsExists(userLogin.username, userLogin.password))
            {
                return true;
            }
            else if (adminRepository.AdminWithCredentialsExists(userLogin.username, userLogin.password))
            {
                return true;
            }

            return false;
        }

        public (string token, UserDto user) GenerateJwt(UserLoginDto userLogin, string secretKey)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var user = userRepository.GetUserByUsername(userLogin.username);
            var admin = adminRepository.GetAdminByUsername(userLogin.username);
            if (user == null && admin == null)
            {
                throw new NullReferenceException("User 0r admin not found.");
            }


            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.NameIdentifier, userLogin.username) 
                
            };

            string role;
            string username, email;
            string name = string.Empty; 
            string surname = string.Empty;

            if (admin != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                role = "Admin";
                username = admin.username;
                email = admin.email;
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                role = "User";
                username = user.username;
                name = user.name;
                surname = user.surname;
                email = user.email;
            }


            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );

            

            

            var userDto = new UserDto
            {
                username = username,
                name = name,
                surname = surname,
                email = email,
                role = role
            };

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return (tokenString, userDto);
        }

       

    }
}
