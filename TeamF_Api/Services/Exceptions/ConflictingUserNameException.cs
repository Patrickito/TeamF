using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamF_Api.Services.Exceptions
{
    public class ConflictingUserNameException : Exception
    {
        public ConflictingUserNameException(string message) : base(message)
        {
        }
    }
}
