using Brazilian.Utility.Net.Api.Configuration;
using Brazilian.Utility.Net.Infra.CrossCutting.IoC;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using Brazilian.Utility.Net.Api.Filters;
using Microsoft.Extensions.Logging;

namespace Brazilian.Utility.Net.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            JsonConvert.DefaultSettings = () => jsonSettings;



            services
                .AddMediatR(typeof(Startup))
                .AddHealthChecks().Services
                .AddControllers(controllers =>
                {
                    controllers.Filters.Add<ExceptionFilter>();
                })
                .AddNewtonsoftJson().Services
                .AddRouting()
                .AddApiVersioning(version => version.ReportApiVersions = true)
                .AddVersionedApiExplorer(version =>
                {
                    version.GroupNameFormat = "'v'VVV";
                    version.SubstituteApiVersionInUrl = true;
                })
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
                .AddSwaggerGen(swagger => { swagger.OperationFilter<SwaggerDefaultValues>(); });

            services
                .ConfigureContainer(Configuration);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider user)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.ConfigureSwagger(Configuration, user)
                .UseRouting()
                .UseCors(option => option.AllowAnyOrigin()
                                            .AllowAnyMethod()
                                            .AllowAnyHeader())
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
