using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamF_Api.DTO
{
    public class RoleChangeDTO
    {
        public string UserName { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}
