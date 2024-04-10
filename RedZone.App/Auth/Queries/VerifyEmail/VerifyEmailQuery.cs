using RedZone.App.Services.Auth.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedZone.App.Auth.Queries.VerifyEmail
{
    
    public record VerifyEmailQuery(
        string UserId,
        string Code) : IRequest<ErrorOr<AuthResult>>;
}
