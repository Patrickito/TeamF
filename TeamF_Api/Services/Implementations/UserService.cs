using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamF_Api.DAL;
using TeamF_Api.DAL.Entity;
using TeamF_Api.Services.Interfaces;

namespace TeamF_Api.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly CAFFShopDbContext _context;

        public UserService(CAFFShopDbContext context)
        {
            _context = context;
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

        public Task RegisterUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
