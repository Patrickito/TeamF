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
using TeamF_Api.Security.PasswordEncoders;

namespace TeamF_Api.Security.Token
{
    public class JwtTokenGenerator : ITokenGenerator
    {
        private readonly CAFFShopDbContext _context;
        private readonly IPasswordEncoder _encoder;
        private readonly SecurityConfiguration _config;

        public JwtTokenGenerator(CAFFShopDbContext context, IPasswordEncoder encoder, IOptions<SecurityConfiguration> config)
        {
            _context = context;
            _encoder = encoder;
            _config = config.Value;
        }

        public string GenerateToken(string userName, string password)
        {
            var user = _context.Users
                .Where(u => u.Name.Equals(userName))
                .FirstOrDefault();

            if (user == null || !_encoder.Verify(password, user.Password))
            {
                throw new AuthenticationException($"Username or password incorrect");
            }

            return GenerateToken(user);
        }

        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(SecurityConstants.UserNameClaim, user.Name),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddHours(_config.TokenExpirationHours)).ToUnixTimeSeconds().ToString()),
            };
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(SecurityConstants.RoleClaim, role.Name));
            }

            var token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Secret)),
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
