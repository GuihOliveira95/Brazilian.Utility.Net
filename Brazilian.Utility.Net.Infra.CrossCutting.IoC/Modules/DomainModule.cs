using FluentValidation;
using Brazilian.Utility.Net.Domain.Common.Behaviors;
using Brazilian.Utility.Net.Domain.Common.Configs;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Brazilian.Utility.Net.Domain.Utility.Services.Interface;
using Brazilian.Utility.Net.Domain.Utility.Services;
using Brazilian.Utility.Net.Domain.Integration.IPVA;
using Brazilian.Utility.Net.Domain.Utility.Integration;
using Brazilian.Utility.Net.Domain.Utility.Queries.GetIPVA;
using Microsoft.Extensions.Configuration;

namespace Brazilian.Utility.Net.Infra.CrossCutting.IoC.Modules
{
    public static class DomainModule
    {
        public static void Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddConfigs(configuration);

            services.AddServices();
            services.AddAcls();

            services.AddHandlers();
            services.AddValidators();
        }

        private static void AddConfigs(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BaseUrlConfig>(options => configuration.GetSection("BaseUrl").Bind(options));

        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<UtilityService>();
            services.AddScoped<IUtilityService, UtilityService>();
        }

        private static void AddAcls(this IServiceCollection services)
        {
            services.AddScoped<IIpvaApi, IpvaApi>();
        }

        private static void AddHandlers(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<GetIPVARequest, GetIPVAResponse>, GetIPVAHandler>();
        }

        private static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddScoped<IValidator<GetIPVARequest>, GetIPVAValidator>();

        }

    }
}
