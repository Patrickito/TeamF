using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamF_Api.Security.PasswordEncoders
{
    public class BcryptPasswordEncoder : IPasswordEncoder
    {
        public string Encrypt(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string givenPassword, string realPassword)
        {
            return BCrypt.Net.BCrypt.Verify(givenPassword, realPassword);
        }
    }
}
