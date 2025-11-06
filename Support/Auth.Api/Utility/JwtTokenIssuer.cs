using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Api.Utility
{
    public static class JwtTokenIssuer
    {
        public static string IssueJwtToken(Guid userId, string key, int tokenTimeoutMinutes)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("userId",userId.ToString()),
                    new Claim("timeout",tokenTimeoutMinutes.ToString())
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256),
                Expires = DateTime.UtcNow.AddMinutes(tokenTimeoutMinutes)
            };
            JwtSecurityTokenHandler handler = new();
            var token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }


    }
}
