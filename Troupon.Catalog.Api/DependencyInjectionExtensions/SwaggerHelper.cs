using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Infra.Api.SwaggerGen;
using Infra.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Troupon.Catalog.Api.DependencyInjectionExtensions
{
  public static class SwaggerHelper
  {
    public static IServiceCollection AddOpenApi(
      this IServiceCollection services, IOAuthSettings apiKeySettings)
    {
      services.AddApiVersioning(cfg =>
      {
        cfg.AssumeDefaultVersionWhenUnspecified = true;
        cfg.DefaultApiVersion = new ApiVersion(1, 0);
        cfg.ReportApiVersions = true;
      });
      services.AddVersionedApiExplorer();

      services.AddSwaggerGen(setup =>
      {
        var descriptionProvider =
              services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();
        foreach (var description in descriptionProvider.ApiVersionDescriptions)
        {
          ConfigureSwaggerGenPerVersion(setup, description);
        }

        setup.OperationFilter<FileUploadOperation>();
        var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
        setup.IncludeXmlComments(xmlCommentsFullPath);

        setup.DocInclusionPredicate((version, apiDescription) =>
        {
          decimal versionMajor = 1;
          var result = decimal.TryParse(
              version,
              NumberStyles.AllowDecimalPoint,
              CultureInfo.InvariantCulture,
              out versionMajor);
          var major = Math.Truncate(versionMajor);

          var values = apiDescription.RelativePath.Split('/').Skip(2);

          apiDescription.RelativePath = $"api/v{major}/{string.Join("/", values)}";

          var versionParam = apiDescription.ParameterDescriptions.SingleOrDefault(p => p.Name == "version");
          if (versionParam != null)
          {
            apiDescription.ParameterDescriptions.Remove(versionParam);
          }

          return true;
        });

        Auth2FiltersAndSecurity(apiKeySettings, setup);

        setup.EnableAnnotations();
        setup.IgnoreObsoleteActions();
        setup.OrderActionsBy(descriptor => descriptor.GroupName);
        setup.ResolveConflictingActions(api => api.First());
      });
      services.Configure<ApiBehaviorOptions>(
            options =>
            {
              options.InvalidModelStateResponseFactory = actionContext =>
              {
                var actionExecutingContext =
                  actionContext as ActionExecutingContext;

                if (actionContext.ModelState.ErrorCount > 0
                    && actionExecutingContext?.ActionArguments.Count ==
                    actionContext.ActionDescriptor.Parameters.Count)
                {
                  return new UnprocessableEntityObjectResult(actionContext.ModelState);
                }

                return new BadRequestObjectResult(actionContext.ModelState);
              };
            });

      return services;
    }

    private static void Auth2FiltersAndSecurity(IOAuthSettings apiKeySettings, SwaggerGenOptions setup)
    {
      setup.SchemaFilter<SchemaFilter>();

      setup.MapType<FileContentResult>(() => new OpenApiSchema { Type = "string", Format = "binary" });
      setup.MapType<IFormFile>(() => new OpenApiSchema { Type = "string", Format = "binary" });
      setup.AddOAuthSecurity(apiKeySettings);
    }

    private static void ConfigureSwaggerGenPerVersion(SwaggerGenOptions setup, ApiVersionDescription description)
    {
      setup.SwaggerDoc(description.GroupName, new OpenApiInfo
      {
        Title = $"Troupon.Catalog Api Specification {description.ApiVersion.ToString()}",
        Description = "Api specification",
        Version = description.ApiVersion.ToString(),
        Contact = new OpenApiContact()
        {
          Email = "oualid.ktata@gmail.com",
          Name = "Oualid Ktata",
        },
        License = new OpenApiLicense()
        {
          Name = "OKT",
          Url = new Uri("https://opensource.org/licenses/MIT"),
        },
      });
    }

    private static void AddOAuth2(IOAuthSettings authSettings, SwaggerUIOptions setup)
    {
      setup.OAuthClientId(authSettings.ClientId);
      setup.OAuthClientSecret(authSettings.ClientSecret);
      setup.OAuthAppName("Portal Api");
    }
  }
}
