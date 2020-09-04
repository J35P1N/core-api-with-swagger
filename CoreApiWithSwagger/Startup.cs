using System.Linq;
using System.Reflection;
using CoreApiWithSwagger.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CoreApiWithSwagger
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure versions 
            services.AddApiVersioning(apiVersioningOptions =>
            {
                apiVersioningOptions.AssumeDefaultVersionWhenUnspecified = true;
                apiVersioningOptions.DefaultApiVersion = new ApiVersion(1, 0);
                apiVersioningOptions.ReportApiVersions = true;
                apiVersioningOptions.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(swaggerGenOptions =>
            {
                var swaggerSettings = new SwaggerOptions();
                Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerSettings);

                foreach (var currentVersion in swaggerSettings.Versions)
                {
                    swaggerGenOptions.SwaggerDoc(currentVersion.Name, new OpenApiInfo
                    {
                        Title = swaggerSettings.Title,
                        Version = currentVersion.Name,
                        Description = swaggerSettings.Description
                    });
                }

                swaggerGenOptions.DocInclusionPredicate((version, desc) =>
                {
                    if (!desc.TryGetMethodInfo(out MethodInfo methodInfo))
                    {
                        return false;
                    }
                    var versions = methodInfo.DeclaringType.GetConstructors()
                        .SelectMany(constructorInfo => constructorInfo.DeclaringType.CustomAttributes
                            .Where(attributeData => attributeData.AttributeType == typeof(ApiVersionAttribute))
                            .SelectMany(attributeData => attributeData.ConstructorArguments
                                .Select(attributeTypedArgument => attributeTypedArgument.Value)));

                    return versions.Any(v => $"{v}" == version);
                });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
            app.UseSwagger(option => option.RouteTemplate = swaggerOptions.JsonRoute);

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(option =>
            {
                foreach (var currentVersion in swaggerOptions.Versions)
                {
                    option.SwaggerEndpoint(currentVersion.UiEndpoint, $"{swaggerOptions.Title} {currentVersion.Name}");
                }
                option.RoutePrefix = string.Empty; // to serve the Swagger UI at the app's root
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
