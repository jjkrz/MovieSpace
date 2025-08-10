using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Common;
using Infrastructure.Persistance.Identity;
using Infrastructure.Persistance.Services.AuthService;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Persistance.Services.Jwt
{
    public class JwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Result<JwtToken> GenerateToken(ApplicationUser user)
        {
            try
            {

                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Email, user.Email!),
                    new(ClaimTypes.Name, user.UserName!)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

                var creds = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

                var expires = DateTime.UtcNow.AddHours(2);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: expires,
                    signingCredentials: creds);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                var result = new JwtToken
                {
                    Token = tokenString,
                    Expiration = expires
                };

                return result;
            }
            catch (Exception ex)
            {
                return Result.Failure<JwtToken>(AuthErrors.TokenGenerationFailed(ex.Message));
            }

        }
    }

    public class JwtToken
    {
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }
    }
}
