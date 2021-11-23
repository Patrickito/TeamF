using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamF_Api.DTO
{
    public class PasswordChangeDTO
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
