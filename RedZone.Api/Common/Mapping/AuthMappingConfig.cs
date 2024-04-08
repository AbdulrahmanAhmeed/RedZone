using RedZone.App.Auth.Commands.Register;
using RedZone.App.Auth.Queries.Login;
using RedZone.App.Services.Auth.Common;
using RedZone.Contracts.Auth;
using Mapster;


namespace RedZone.api.Common.Mapping
{
    public class AuthMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();
            config.NewConfig<LoginRequest, LoginQuery>();
            config.NewConfig<AuthResult, AuthResponse>()
                .Map(des=>des.Token,src=>src.Token)
                .Map(des=>des.refreshToken,src=>src.refreshToken)
                .Map(des=>des.isAdmin,scr=>scr.isAdmin)
                .Map(des=>des.status,src=>src.status);
        }
    }
}
