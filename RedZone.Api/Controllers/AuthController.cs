using RedZone.App.Auth.Commands.Register;
using RedZone.App.Auth.Queries.Login;

using RedZone.App.Services.Auth.Common;
using RedZone.Contracts.Auth;
using ErrorOr;
using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RedZone.App.Auth.Queries.RefreshToken;
using Microsoft.AspNetCore.Identity;

namespace RedZone.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        //private readonly IAuthQueryService _authQueryService;
        //private readonly IAuthCommandService _authCommandService;
        public AuthController(IMediator mediator,IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
            //_authQueryService = authQueryService;
            //_authCommandService = authCommandService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var Command = _mapper.Map<RegisterCommand>(request);
            ErrorOr<AuthResult> result = await _mediator.Send(Command);
            return result.Match(
                result => Ok(_mapper.Map<AuthResponse>(result)),
                Errors => Problem(Errors)
                );
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = _mapper.Map<VerifyEmailQuery>(request); 
            ErrorOr<AuthResult> result = await _mediator.Send(query);
            return result.Match(
                result => Ok(_mapper.Map<AuthResponse>(result)),
                Errors => Problem(Errors)
                );
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
        {
            var query = _mapper.Map<RefreshTokenQuery>(request);
            ErrorOr<AuthResult> result = await _mediator.Send(query);
            return result.Match(
                result => Ok(_mapper.Map<AuthResponse>(result)),
                Errors => Problem(Errors)
                );
        }

    }
}
