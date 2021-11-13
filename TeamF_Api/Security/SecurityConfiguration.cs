using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamF_Api.Security
{
    public class SecurityConfiguration
    {
        public string Secret { get; set; }
        public int TokenExpirationHours { get; set; }
    }
}
