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

namespace RedZone.App.Auth.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<AuthResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            if (_userRepository.GetUserByEmail(query.Email) is not User user)
            {
                return Errors.Auth.UnvalidEmail;
            }
            if (!_userRepository.Login(query.Email,query.password))
            {
                return new[] { Errors.Auth.UnvalidPassword };
            }
            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthResult("Auth", "dc", false, token);
        }
    }
}
