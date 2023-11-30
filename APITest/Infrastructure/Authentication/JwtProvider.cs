using APITest.Application.DTOs.Response;
using APITest.Application.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APITest.Infrastructure.Authentication
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtSettings _jwtSettings;

        public JwtProvider(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateToken(AccountSignInRes obj)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, obj.account_id.ToString()),
                new Claim(ClaimTypes.Email, obj.email),
                new Claim(ClaimTypes.Name, obj.name),
                new Claim("/UserID", obj.account_id.ToString())
                // Add other claims as needed
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                null,
                DateTime.Now.AddHours(1),
                signingCredentials);

            string tokenValue = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return tokenValue;
        }
    }
}
