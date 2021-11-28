using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamF_Api.DAL.Entity;
using TeamF_Api.DTO;

namespace TeamF_Api.Services.Interfaces
{
    public interface IUserService
    {
        Task RegisterUser(string userName, string password);

        Task<string> Login(string userName, string password);

        Task<User> FindUserByName(string name);

        Task DeleteUser(string id);

        Task ChangePassword(string userName, string oldPassword, string newPassword);

        Task UpdateRoles(string userName, ICollection<string> roles);

        Task<ICollection<UserDTO>> FetchAllUsers();
    }
}
