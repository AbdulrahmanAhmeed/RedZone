using ErrorOr;
using MediatR;
using RedZone.App.Services.Auth.Common;


namespace RedZone.App.Auth.Queries.RefreshToken
{
    public record RefreshTokenQuery(
        string Token,
        string RefreshToken
        ) : IRequest<ErrorOr<AuthResult>>;
}
