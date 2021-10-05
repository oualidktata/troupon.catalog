using System;
using System.Collections.Generic;
using Infra.oAuthService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Troupon.Catalog.Api.Authentication
{
  public static class BearerOpenApiExtensions
  {
    public static void AddBearerAuthentication(
      this Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions setup,
      IOAuthSettings oAuthSettings)
    {
      setup.AddSecurityDefinition(
        oAuthSettings.Scheme,
        new OpenApiSecurityScheme
        {
          Type = SecuritySchemeType.Http,
          Name = oAuthSettings.AuthHeaderName,
          In = ParameterLocation.Header,

          // BearerFormat = "JWT",
          Scheme = oAuthSettings.Scheme,
          Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer'[space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        });

      setup.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
          {
            new OpenApiSecurityScheme
            {
              Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = oAuthSettings.Scheme,
              },
            },
            new List<string>()
          },
        });
    }

    public static void AddOAuthSecurity(this Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions setup, IOAuthSettings oauthSettings)
    {
      var flows = new OpenApiOAuthFlows();
      flows.ClientCredentials = new OpenApiOAuthFlow()
      {
        TokenUrl = new Uri(oauthSettings.TokenUrl, UriKind.Relative),
        Scopes = oauthSettings.Scopes,
      };
      var oauthScheme = new OpenApiSecurityScheme()
      {
        Type = SecuritySchemeType.OAuth2,
        Description = "OAuth2 Description",
        Name = oauthSettings.AuthHeaderName,
        In = ParameterLocation.Query,
        Flows = flows,
        Scheme = oauthSettings.Scheme,
      };

      setup.AddSecurityDefinition("Bearer", oauthScheme);

      var securityrRequirements = new OpenApiSecurityRequirement();
      securityrRequirements.Add(oauthScheme, new List<string>() { });
      setup.AddSecurityRequirement(securityrRequirements);
    }
  }
}
