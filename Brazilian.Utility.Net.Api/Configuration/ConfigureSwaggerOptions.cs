using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Brazilian.Utility.Net.Api.Configuration
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _user;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider user)
        {
            _user = user;
        }

        public void Configure(SwaggerGenOptions options)
        {
            _user.ApiVersionDescriptions
                .ToList()
                .ForEach(version =>
               {
                   var info = new OpenApiInfo
                   {
                       Title = "Brazilian.Utility.Net - API",
                       Version = version.ApiVersion.ToString()
                   };
                   options.SwaggerDoc(version.GroupName, info);
               });
        }

    }
}
