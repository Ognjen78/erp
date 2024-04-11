using ErpProject.DTO;
using ErpProject.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using System.Text;


namespace WebApplication5.Helpers
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        private readonly IConfiguration configuration;
        private readonly IUserRepository userRepository;

        public AuthenticationHelper(IConfiguration configuration, IUserRepository userRepository)
        {
            this.configuration = configuration;
            this.userRepository = userRepository;
        }

        public bool AuthenticatePrincipal(UserLoginDto userLogin)
        {
            if (userRepository.UserWithCredentialsExists(userLogin.username, userLogin.password))
            {
                return true;
            }

            return false;
        }

        public string GenerateJwt(UserLoginDto userLogin)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                                             configuration["Jwt:Issuer"],
                                             null,
                                             expires: DateTime.Now.AddMinutes(120),
                                             signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
