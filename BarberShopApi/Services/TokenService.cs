using BarberShopApi.Domain.Entities;
using BarberShopApi.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BarberShopApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration; 
        }
        

        public  string GenerateToken(User user, string role)
        {

        string securityKey = _configuration["SecurityKey"]!;

        var tokenHandle = new JwtSecurityTokenHandler();
        var encodingKey = Encoding.ASCII.GetBytes(securityKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new []
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, role)    
                  
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(encodingKey), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandle.CreateToken(tokenDescriptor);

            return tokenHandle.WriteToken(token);
        }
    }
}
