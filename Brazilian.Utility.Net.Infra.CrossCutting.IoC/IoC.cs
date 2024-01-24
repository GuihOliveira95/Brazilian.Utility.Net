using Brazilian.Utility.Net.Infra.CrossCutting.IoC.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Brazilian.Utility.Net.Infra.CrossCutting.IoC
{
    public static class IoC
    {
         public static IServiceCollection ConfigureContainer(this IServiceCollection services, IConfiguration configuration )
        {
            DataModule.Register(services, configuration);
            DomainModule.Register(services, configuration);
            InfrastructureModule.Register(services);

            return services;
        }
    }
}
