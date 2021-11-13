using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamF_Api.DAL;
using TeamF_Api.DAL.Entity;
using TeamF_Api.Security;
using TeamF_Api.Security.PasswordEncoders;
using TeamF_Api.Services.Exceptions;
using TeamF_Api.Services.Interfaces;

namespace TeamF_Api.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly CAFFShopDbContext _context;
        private readonly IPasswordEncoder _encoder;

        public UserService(CAFFShopDbContext context, IPasswordEncoder encoder)
        {
            _context = context;
            _encoder = encoder;
        }

        public async Task DeleteUser(long id)
        {
            var user = await _context.Users.FindAsync(id);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> FindUserByName(string name)
        {
            return await _context.Users.Where(u => u.Name.Equals(name)).FirstOrDefaultAsync();
        }

        public Task<string> Login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterUser(string userName, string password)
        {
            var oldUser = await _context.Users
                .Where(u => u.Name.Equals(userName))
                .FirstOrDefaultAsync();
            if (oldUser != null)
            {
                throw new ConflictingUserNameException($"User already exists with name: {userName}");
            }

            var baseRole = await _context.Roles
                .Where(r => r.Name.Equals(SecurityConstants.BaseUserRole))
                .FirstOrDefaultAsync();
            var newUser = new User
            {
                Name = userName,
                Password = _encoder.Encrypt(password),
                Roles = new List<Role> { baseRole }
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            var oldData = _context.Users.Find(user.Id);
            if (oldData == null)
            {
                await RegisterUser(user.Name, user.Password);
            }
            else
            {
                user.Password = _encoder.Encrypt(user.Password);
                _context.Entry(oldData).CurrentValues.SetValues(user);
            }

            await _context.SaveChangesAsync();
        }
    }
}
