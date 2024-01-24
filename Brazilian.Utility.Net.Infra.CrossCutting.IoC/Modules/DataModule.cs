using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Brazilian.Utility.Net.Infra.CrossCutting.IoC.Modules
{
    public static class DataModule
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories();

        }

        private static void AddRepositories( this IServiceCollection services)
        {
        }
    }
}
