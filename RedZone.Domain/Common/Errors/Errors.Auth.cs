using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedZone.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Auth
        {
            public static Error UnvalidEmail = Error.Conflict(code: "User.UnvalidEmail", description: "Unvalid email");
            public static Error UnvalidPassword = Error.Conflict(code: "User.UnvalidPassword", description: "Unvalid password");
            public static Error UnvalidRefreshToken = Error.Conflict(code: "User.Unvalid", description: "Please login again");
        }
        
    }
}
