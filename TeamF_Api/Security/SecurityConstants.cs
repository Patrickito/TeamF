using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamF_Api.Security
{
    public class SecurityConstants
    {
        public static readonly string UserNameClaim = "Name";
        public static readonly string RoleClaim = "Roles";

        public static readonly string AdminPolicy = "AdminRequired";

        public static readonly string BaseUserRole = "BaseUser";
        public static readonly string AdminRole = "Administrator";
    }
}
