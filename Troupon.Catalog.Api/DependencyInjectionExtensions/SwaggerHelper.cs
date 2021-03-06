using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Infra.oAuthService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Troupon.Catalog.Api.Authentication;

namespace Troupon.Catalog.Api.DependencyInjectionExtensions
{
  public static class SwaggerHelper
  {
    public static IServiceCollection AddOpenApi(
      this IServiceCollection services,IOAuthSettings apiKeySettings)
    {
      services.AddApiVersioning(cfg =>
      {
        cfg.AssumeDefaultVersionWhenUnspecified = true;
        cfg.DefaultApiVersion = new ApiVersion(1, 0);
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
        //setup.DocInclusionPredicate((docName, apiDesc) =>
        //{
        //  if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;
        //  var versions = methodInfo.DeclaringType
        //      .GetCustomAttributes(true);
        //  return true;

        //  //if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

        //  //var versions = methodInfo.DeclaringType
        //  //    .GetCustomAttributes(true)
        //  //    .OfType<ApiVersionAttribute>()
        //  //    .SelectMany(attr => attr.Versions);

        //  //return versions.Any(v => $"v{v.ToString()}" == docName);
        //});

        //setup.DefaultModelExpandDepth(2);
        //setup.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
        //setup.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        //setup.EnableDeepLinking();
        //setup.DisplayOperationId();
        setup.OperationFilter<FileUploadOperation>();
        var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
        setup.IncludeXmlComments(xmlCommentsFullPath);





        setup.DocInclusionPredicate((version, apiDescription) =>
        {

          Decimal versionMajor=1;
          var result = Decimal.TryParse(version,
          NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out versionMajor);
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
        #region Auth2 filters and Security
        setup.SchemaFilter<SchemaFilter>();

        setup.MapType<FileContentResult>(() => new OpenApiSchema { Type = "string", Format = "binary" });
        setup.MapType<IFormFile>(() => new OpenApiSchema { Type = "string", Format = "binary" });
        //setup.DocumentFilter<SecurityRequirementDocumentFilter>();
        setup.AddBearerAuthentication(apiKeySettings); //Uncomment to enableAuth
        #endregion

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
          Name = "Oualid Ktata"
        },
        License = new OpenApiLicense()
        {
          Name = "OKT",
          Url = new Uri("https://opensource.org/licenses/MIT")
        }
      });
    }

    public static void ConfigureSwaggerUI(this IApplicationBuilder app,
      IApiVersionDescriptionProvider apiVersionDescriptionProvider,
      IOAuthSettings authSettings)
    {
      //apiVersionDescriptionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
      app.UseSwaggerUI(setup =>
      {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
          setup.SwaggerEndpoint($"/swagger/{description.ApiVersion}/swagger.json", $"Troupon Catalog Specification{description.ApiVersion.ToString()}");
          setup.RoutePrefix = string.Empty;
        }
        setup.RoutePrefix = string.Empty;
        #region oAuth2
        //setup.OAuthUseBasicAuthenticationWithAccessCodeGrant();
        //setup.OAuth2RedirectUrl(OAuthSettings.ClientId);
        setup.OAuthClientId(authSettings.ClientId);
        setup.OAuthClientSecret(authSettings.ClientSecret);
        setup.OAuthAppName("Portal Api");
        //TODO: remove additional querystring, testing purposes
        //var clientCreds = System.Text.Encoding.UTF8.GetBytes($"{OAuthSettings.ClientId}:{OAuthSettings.ClientSecret}");
        //setup.OAuthAdditionalQueryStringParams( new Dictionary<string, string> { { OAuthSettings.AuthHeaderName,$"Basic {System.Convert.ToBase64String(clientCreds)}" } } );
        //setup.OAuth2RedirectUrl(new Uri(OAuthSettings.CallBackUrl, UriKind.Relative).ToString());
        //setup.OAuthAdditionalQueryStringParams( new Dictionary<string, string> { { "client_id",OAuthSettings.ClientId } } );

        #endregion
    
      });



    }
    //private static void ApplyDocInclusions(SwaggerGenOptions swaggerGenOptions)
    //{
    //  swaggerGenOptions.DocInclusionPredicate((docName, apiDesc) =>
    //  {
    //    var versions = apiDesc.CustomAttributes()
    //        .OfType<ApiVersionAttribute>()
    //        .SelectMany(attr => attr.Versions);

    //    return versions.Any(v => $"v{v.ToString()}" == docName);
    //  });
    //}

    //private static IEnumerable<string> GetApiVersions(Assembly webApiAssembly)
    //{
    //  var apiVersion = webApiAssembly.DefinedTypes
    //      .Where(x => x.IsSubclassOf(typeof(Controller)) && x.GetCustomAttributes<ApiVersionAttribute>().Any())
    //      .Select(y => y.GetCustomAttribute<ApiVersionAttribute>())
    //      .SelectMany(v => v.Versions)
    //      .Distinct()
    //      .OrderBy(x => x);

    //  return apiVersion.Select(x => x.ToString());
    //}
  }
}
