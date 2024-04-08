using RedZone.App.Services.Auth.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedZone.App.Auth.Queries.Login
{
    
    public record LoginQuery(
        string Email,
        string password) : IRequest<ErrorOr<AuthResult>>;
}
