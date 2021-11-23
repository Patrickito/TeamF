using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamF_Api.DAL.Entity;

namespace TeamF_Api.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}
