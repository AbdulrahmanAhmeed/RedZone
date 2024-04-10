using RedZone.App.Common.Interfaces.Auth;
using RedZone.App.Common.Interfaces.Persistence;
using RedZone.App.Services.Auth.Common;
using RedZone.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedZone.Domain.Users;

namespace RedZone.App.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            if (_userRepository.GetUserByEmail(command.Email) != null)
            {
                return Errors.Users.DuplicateEmail;
            }
            var user = User.Create(
            command.Name,
            command.PhoneNumber,
            command.Email);

            _userRepository.AddUser(user,command.password);
            _userRepository.Login(user.Email!, command.password);
            var token = _jwtTokenGenerator.GenerateToken(user);
            var refreshToken = _jwtTokenGenerator.RefreshTokenGeneration();
            return new AuthResult("Auth",refreshToken,false, token);
        }
    }
}
