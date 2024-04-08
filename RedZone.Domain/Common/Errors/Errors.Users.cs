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
        public static class Users
        {
            public static Error DuplicateEmail = Error.Conflict(code: "User.DuplicateEmail", description: "Email is already in use");
        }
    }
}
