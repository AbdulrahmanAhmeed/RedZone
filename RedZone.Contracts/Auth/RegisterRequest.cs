using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedZone.Contracts.Auth
{
    public record RegisterRequest (
        string Name,
        string PhoneNumber,
        string Email,
        string password
        );
}
