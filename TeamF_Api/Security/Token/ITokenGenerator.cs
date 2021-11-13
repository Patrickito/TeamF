using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamF_Api.Security.Token
{
    public interface ITokenGenerator
    {
        string GenerateToken(string userName, string password);
    }
}
