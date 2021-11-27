using Castle.Core.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using TeamF_Api.DAL;
using TeamF_Api.DAL.Entity;
using TeamF_Api.Security;
using TeamF_Api.Security.Token;
using TeamF_Api.Services.Exceptions;
using TeamF_Api.Services.Interfaces;

namespace TeamF_Api.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly UserManager<User> _userManager;
        private readonly CAFFShopDbContext _context;

        public UserService(ITokenGenerator tokenGenerator, UserManager<User> userManager, CAFFShopDbContext context)
        {
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
            _context = context;
        }

        public async Task ChangePassword(string userName, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(userName);
            await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
        }

        public async Task<User> FindUserByName(string name)
        {
            return await _userManager.FindByNameAsync(name);
        }

        public async Task<string> Login(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new AuthenticationException($"User not found or incorrect password for user: {userName}");
            }
            var isValid = await _userManager.CheckPasswordAsync(user, password);
            if (!isValid)
            {
                throw new AuthenticationException($"User not found or incorrect password for user: {userName}");
            }

            var tokenData = new TokenData
            {
                UserName = user.UserName,
                Roles = user.Roles.Select(u => u.Name).ToList()
            };
            return _tokenGenerator.GenerateToken(tokenData);
        }

        public async Task RegisterUser(string userName, string password)
        {
            var baseUser = _context.Roles.FirstOrDefault(r => r.Name.Equals(SecurityConstants.BaseUserRole));
            var admin = _context.Roles.FirstOrDefault(r => r.Name.Equals(SecurityConstants.AdminRole));

            var isFirstUser = !_context.Users.Any();
            var userRoles = isFirstUser ? new List<Role> { baseUser, admin } : new List<Role> { baseUser };

            var user = new User
            {
                UserName = userName,
                Roles = userRoles
            };

            var result = await _userManager.CreateAsync(user, password);
        }

        public async Task UpdateRoles(string userName, ICollection<string> roles)
        {
            var user = await _userManager.FindByNameAsync(userName);
            user.Roles = roles.Select(r => _context.Roles.FirstOrDefault(role => role.Name.Equals(r)))
                .Where(r => r != null)
                .ToList();

            await _userManager.UpdateAsync(user);
        }
    }
}
