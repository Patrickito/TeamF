using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TeamF_Api.Security;

namespace TeamF_Api.Controllers
{
    public class ControllerBase : Controller
    {
        protected string GetUserName()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Name));

            if (claim == null)
            {
                throw new UnauthorizedAccessException("UserName not found");
            }

            return claim.Value;
        }
        protected ICollection<string> GeClaims()
        {
            var claim = User.Claims.Where(c => c.Type.Equals(ClaimTypes.Role));

            if (claim.IsNullOrEmpty())
            {
                throw new UnauthorizedAccessException("Roles not found");
            }

            return claim.Select(c => c.Value).ToList();
        }
    }
}
