using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedZone.Contracts.Auth
{
    public record AuthResponse(
        string status,
        string refreshToken,
        bool isAdmin,
        string Token
        );
}
