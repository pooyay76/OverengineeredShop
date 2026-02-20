using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Api.Infrastructure
{
    public static class JwtTokenIssuer
    {
        public static string IssueJwtToken(Guid userId, SigningCredentials signingCredentials, string issuer,
            string audience, int tokenTimeoutMinutes)
        {
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId",userId.ToString()),
                }),
                SigningCredentials = signingCredentials,
                Expires = DateTime.UtcNow.AddMinutes(tokenTimeoutMinutes),
                Issuer = issuer,
                Audience = audience,
                TokenType = "at+jwt"

            };
            JwtSecurityTokenHandler handler = new();
            var token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }


    }
}
