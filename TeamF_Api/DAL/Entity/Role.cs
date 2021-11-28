using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamF_Api.DAL.Entity
{
    public class Role : IdentityRole<Guid>
    {
    }
}
