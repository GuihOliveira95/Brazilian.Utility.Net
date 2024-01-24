using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Brazilian.Utility.Net.Api.Configuration
{
    public static class SwaggerExtensions
    {
        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app, IConfiguration config,
            IApiVersionDescriptionProvider user)
        {
            var useSwagger = config.GetValue<bool>("UseSwagger");

            if (!useSwagger) return app;

            return app
                .UseSwagger()
                .UseSwaggerUI(swagger => user.ApiVersionDescriptions
                    .ToList()
                    .ForEach(f => swagger.SwaggerEndpoint($"/swagger/{f.GroupName}/swagger.json", f.GroupName.ToUpper())));
        }

    }
}
