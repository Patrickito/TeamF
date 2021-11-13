using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamF_Api.DAL;
using TeamF_Api.DAL.Entity;
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

        public async Task RegisterUser(User user)
        {
            var oldUser = await FindUserByName(user.Name);
            if (oldUser != null)
            {
                throw new ConflictingUserNameException($"User already exists with name: {user.Name}");
            }

            user.Password = _encoder.Encrypt(user.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            var oldData = _context.Users.Find(user.Id);
            if (oldData == null)
            {
                await RegisterUser(user);
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
