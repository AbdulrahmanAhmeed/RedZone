using RedZone.App.Auth.Commands.Register;
using RedZone.App.Common.Interfaces.Auth;
using RedZone.App.Common.Interfaces.Persistence;
using RedZone.App.Services.Auth.Common;
using ErrorOr;
using RedZone.Domain.Common.Errors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedZone.Domain.Users;

namespace RedZone.App.Auth.Queries.RefreshToken
{
    public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, ErrorOr<AuthResult>>
    {
        private readonly IUserRepository _userRepository;
        public RefreshTokenQueryHandler( IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<AuthResult>> Handle(RefreshTokenQuery query, CancellationToken cancellationToken)
        {
            
            var result = _userRepository.RefreshToken(query.Token, query.RefreshToken);
            if (result is null)
            {
                return new[] { Errors.Auth.UnvalidRefreshToken };
            }
            
            return result;
        }
    }
}
