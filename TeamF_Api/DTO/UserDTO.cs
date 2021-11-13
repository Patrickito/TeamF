using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamF_Api.DAL.Entity;

namespace TeamF_Api.DTO
{
    public class UserDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
