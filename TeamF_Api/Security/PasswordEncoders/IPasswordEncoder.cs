using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamF_Api.Security.PasswordEncoders
{
    public interface IPasswordEncoder
    {
        string Encrypt(string password);
        bool Verify(string givenPassword, string realPassword);
    }
}
