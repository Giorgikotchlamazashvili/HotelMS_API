using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HotelMS.Application.Interfaces.Services;
using HotelMS.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HotelMS.Infrastructure.Security
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration config;

        public TokenGenerator(IConfiguration _config)
        {
            config = _config;
        }

        public string GenerateAccessToken(Users user)
        {
            var secret = config["Jwt:Secret"];
            var issuer = config["Jwt:Issuer"];
            var audience = config["Jwt:Audience"];
            var minutes = int.Parse(config["Jwt:AccessTokenMinutes"]);

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secret));

            var creds = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserRole.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(minutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
    }
}