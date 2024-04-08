using RedZone.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedZone.App.Common.Interfaces.Auth
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(User user);
    }
}
