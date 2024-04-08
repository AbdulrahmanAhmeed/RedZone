using RedZone.App.Services.Auth.Common;
using ErrorOr;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedZone.App.Auth.Commands.Register
{

    public record RegisterCommand(
        string Name,
        string PhoneNumber,
        string Email,
        string password) : IRequest<ErrorOr<AuthResult>>;
}

