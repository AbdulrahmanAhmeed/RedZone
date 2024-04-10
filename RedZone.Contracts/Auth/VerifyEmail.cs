using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedZone.Contracts.Auth
{
    public record VerifyEmail(
        string UserId,
        string Code
        );
}
