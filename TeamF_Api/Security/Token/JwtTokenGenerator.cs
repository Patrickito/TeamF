using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TeamF_Api.DAL;
using TeamF_Api.DAL.Entity;

namespace TeamF_Api.Security.Token
{
    public class JwtTokenGenerator : ITokenGenerator
    {
        private readonly SecurityConfiguration _config;

        public JwtTokenGenerator(IOptions<SecurityConfiguration> config)
        {
            _config = config.Value;
        }

        public string GenerateToken(TokenData data)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, data.UserName),
            };
            foreach (var role in data.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_config.TokenExpirationInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
