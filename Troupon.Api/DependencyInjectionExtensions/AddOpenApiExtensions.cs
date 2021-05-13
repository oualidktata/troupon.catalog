using Infra.oAuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using Troupon.Catalog.Service.Api.Authentication;

namespace Troupon.Catalog.Service.Api.DependencyInjectionExtensions
{
    public static class AddOpenApiExtensions
    {
        public static IServiceCollection AddOpenApiToApplication(this IServiceCollection services,IConfiguration configuration)
        {

            var apiKeySettings = new OAuthSettings ();
            configuration.GetSection("Auth:KeyCloackProvider").Bind(apiKeySettings);
            services.AddSwaggerGen(setup => {
                setup.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Troupon Catalog API",
                    Description = "A simple API to manage a Catalog for prodcuts and services",
                    TermsOfService = new Uri("https://example.com/terms"),
                    
                    Contact = new OpenApiContact
                    {
                        Name = "Oualid Ktata",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/oualidktata/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                setup.IncludeXmlComments(xmlPath);
                #region Auth2 filters and Security
                //setup.SchemaFilter<SchemaFilter>();

                setup.MapType<FileContentResult>(() => new OpenApiSchema { Type = "string", Format = "binary" });
                setup.MapType<IFormFile>(() => new OpenApiSchema { Type = "string", Format = "binary" });
                //setup.DocumentFilter<SecurityRequirementDocumentFilter>();
                setup.AddBearerAuthentication(apiKeySettings);//Uncomment to enableAuth

                #endregion

            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var actionExecutingContext =
                        actionContext as ActionExecutingContext;

                    if (actionContext.ModelState.ErrorCount > 0
                        && actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
                    {
                        return new UnprocessableEntityObjectResult(actionContext.ModelState);
                    }

                    return new BadRequestObjectResult(actionContext.ModelState);
                };
            });
            return services;
        }
    }
}
