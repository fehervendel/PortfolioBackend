using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DotNetEnv;
using Microsoft.IdentityModel.Tokens;

namespace Portfolio.Services
{
    public class AuthService
    {
        public string GenerateJwt()
        {
            Env.Load();
            string jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
            var key = Encoding.ASCII.GetBytes(jwtSecret);

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, "admin")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return handler.WriteToken(token);
        }
    }
}