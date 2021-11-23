using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamF_Api.Security
{
    public class TokenData
    {
        public string UserName { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}
