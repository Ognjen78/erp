using ErpProject.DTO;
using ErpProject.Interface;
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

        public string GenerateJwt(UserLoginDto userLogin, string secretKey)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            
            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.NameIdentifier, userLogin.username) 
                
            };

            if (adminRepository.AdminWithCredentialsExists(userLogin.username, userLogin.password)) 
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
            }


            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

       

    }
}
