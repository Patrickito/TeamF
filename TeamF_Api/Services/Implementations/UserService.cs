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
using TeamF_Api.DTO;
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

        public async Task<ICollection<string>> FetchAllRoles()
        {
            return await _context.Roles.Select(r => r.Name).ToListAsync();
        }

        public async Task<ICollection<UserDTO>> FetchAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var res = new List<UserDTO>(users.Count);

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                res.Add(new UserDTO
                {
                    Id = user.Id.ToString(),
                    UserName = user.UserName,
                    Roles = roles
                });
            }

            return res;
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

            var roles = await _userManager.GetRolesAsync(user);
            var tokenData = new TokenData
            {
                UserName = user.UserName,
                Roles = roles
            };
            return _tokenGenerator.GenerateToken(tokenData);
        }

        public async Task RegisterUser(string userName, string password)
        {
            var isFirstUser = !_context.Users.Any();

            var user = new User
            {
                UserName = userName
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, SecurityConstants.BaseUserRole);
                if (isFirstUser)
                {
                    await _userManager.AddToRoleAsync(user, SecurityConstants.AdminRole);
                }
            }
        }

        public async Task UpdateRoles(string userName, ICollection<string> roles)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var currentRoles = await _userManager.GetRolesAsync(user);

            var rolesToAdd = roles.Where(r => !currentRoles.Contains(r));
            var rolesToRemove = currentRoles.Where(r => !roles.Contains(r));

            foreach (var role in rolesToAdd)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            foreach (var role in rolesToRemove)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }
        }
    }
}
