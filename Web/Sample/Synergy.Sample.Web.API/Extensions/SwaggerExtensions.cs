using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Synergy.Sample.Web.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddVersionedSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(
                c =>
                {
                    var serviceProvider = services.BuildServiceProvider();
                    var provider = serviceProvider.GetRequiredService<IApiVersionDescriptionProvider>();
                    var environment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

                    // Add a swagger document for each discovered API version  
                    foreach (var apiVersion in provider.ApiVersionDescriptions)
                    {
                        c.SwaggerDoc(apiVersion.GroupName, SwaggerExtensions.GenerateSwaggerVersionInfo(apiVersion, environment));
                    }

                    // TODO: Dodaj filtry

                    foreach (var assembly in Application.GetApplicationAssemblies())
                    {
                        var xmlFile = $"{assembly.GetName().Name}.xml";
                        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                        if (File.Exists(xmlPath))
                        {
                            c.IncludeXmlComments(xmlPath);
                        }
                    }

                    c.CustomSchemaIds(SwaggerExtensions.GetSchemaId);
                });
        }

        private static OpenApiInfo GenerateSwaggerVersionInfo(ApiVersionDescription api, IWebHostEnvironment environment)
        {
            return new OpenApiInfo
                   {
                       Version = api.ApiVersion.ToString(),
                       Title = "Synergy sample API",
                       Description = SwaggerExtensions.GetApiVersionDescription(api, environment),
                       //TermsOfService = new Uri("https://github.com/synergy-software/net-api-best-practices/blob/master/LICENSE"),
                       Contact = new OpenApiContact
                                 {
                                     Name = "Synergy software",
                                     Email = "synergy@todo.com",
                                     Url = new Uri("https://github.com/synergy-software")
                                 },
                       License = new OpenApiLicense
                                 {
                                     Name = "Use under MIT License",
                                     Url = new Uri("https://github.com/synergy-software/net-api-best-practices/blob/master/LICENSE")
                                 }
                   };
        }

        private static string GetApiVersionDescription(ApiVersionDescription api, IWebHostEnvironment environment)
        {
            var application = Application.GetApplicationInfo();
            var createdOn = application.CreatedOn.ToString();
            if (environment.IsDevelopment() || environment.IsTests())
            {
                createdOn = "DEVELOPERS MACHINE";
            }

            return $"<label>API Version</label>: <strong>{api.ApiVersion} {(api.IsDeprecated ? "(DEPRECATED)" : "")}</strong><br/> " +
                   $"<label>Application Name</label>: <strong>{application.ProductName}</strong><br/> " +
                   $"<label>Application Version</label>: <strong>{application.FileVersion}</strong><br/> " +
                   $"<label>Application Created on</label>: <strong>{createdOn}</strong>";
        }

        private static string GetSchemaId(Type type)
        {
            if (type.DeclaringType == null)
            {
                return type.Name;
            }

            return SwaggerExtensions.GetSchemaId(type.DeclaringType) + "." + type.Name;
        }

        public static void UseVersionedSwaggerUI(this IApplicationBuilder app)
        {
            var apiVersionProvider = app.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
            app.UseSwaggerUI(
                c =>
                {
                    // Build a swagger endpoint for each discovered API version
                    foreach (var description in apiVersionProvider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
        }
    }
}