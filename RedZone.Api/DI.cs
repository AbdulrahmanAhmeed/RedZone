using RedZone.api.Common.Errors;
using RedZone.api.Common.Mapping;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RedZone.api.Common.Errors;
using System.Reflection;

namespace RedZone.Api
{
    public static class DI
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddMappings();
            services.AddControllers();

            services.AddSingleton<ProblemDetailsFactory, RedZoneProblemDetailsFactory>();
            return services;
        }
    }
}
