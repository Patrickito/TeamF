using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamF_Api.DAL.Entity;

namespace TeamF_Api.Services.Interfaces
{
    public interface IUserService
    {
        Task RegisterUser(User user);

        Task<User> FindUserByName(string name);

        Task UpdateUser(User user);

        Task DeleteUser(long id);
    }
}
