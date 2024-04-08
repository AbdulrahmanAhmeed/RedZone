using RedZone.App.Auth.Commands.Register;
using RedZone.App.Common.Behaviors;
using RedZone.App.Services.Auth.Common;
using ErrorOr;
using MediatR;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace RedZone.App
{
    public static class DI
    {
        public static IServiceCollection AddApps(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DI).Assembly));
            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));
            
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //services.AddScoped<IAuthQueryService, AuthQueryService>();
            //services.AddScoped<IAuthCommandService, AuthCommandService>();

            return services;
        }
    }
}
