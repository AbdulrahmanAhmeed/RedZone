using RedZone.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedZone.App.Services.Auth.Common
{
    public record AuthResult(
        string status,
        string refreshToken,
        bool isAdmin,
        string Token
        );
}
