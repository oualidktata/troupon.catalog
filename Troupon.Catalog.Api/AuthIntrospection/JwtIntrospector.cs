using System;
using System.Linq;
using Infra.oAuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Troupon.Catalog.Api.Authentication;

namespace Troupon.Catalog.Api.AuthIntrospection
{
#pragma warning disable SA1649 // File name should match first type name
  public interface IJwtIntrospector
#pragma warning restore SA1649 // File name should match first type name
  {
    JwtIntrospection GetJwtIntrospection();
  }

  public class JwtIntrospector : IJwtIntrospector
  {
    private static readonly string[] SupportedAuthorizationSchemes = { "Bearer", "SSWS" };

    private IHttpContextAccessor HttpContextAccessor { get; }

    private IOAuthSettings OAuthSettings { get; }

    public JwtIntrospector(IHttpContextAccessor httpContextAccessor, IOAuthSettings oAuthSettings)
    {
      HttpContextAccessor = httpContextAccessor;
      OAuthSettings = oAuthSettings;
    }

    public JwtIntrospection GetJwtIntrospection()
    {
      var authorizationHeader = HttpContextAccessor.HttpContext?.GetRequestAuthorizationHeader();
      if (!AuthorizationExists(authorizationHeader))
      {
        throw JwtIntrospectionException.AuthorizationHeaderMissing();
      }

      string accessToken = GetAccessToken(authorizationHeader!);
      return new JwtIntrospection(accessToken, OAuthSettings.ClientId);
    }

    private static bool AuthorizationExists(string? authorizationHeader)
    {
      return !string.IsNullOrEmpty(authorizationHeader);
    }

    private static string GetAccessToken(string authorizationHeader)
    {
      var authorizationScheme = authorizationHeader.ExtractAuthorizationScheme();
      if (!SupportedAuthorizationScheme(authorizationScheme))
      {
        throw JwtIntrospectionException.UnsupportedAuthorizationSchemes(SupportedAuthorizationSchemes);
      }

      return authorizationHeader.ExtractAuthorizationToken(authorizationScheme);
    }

    private static bool SupportedAuthorizationScheme(string authorizationScheme)
    {
      return SupportedAuthorizationSchemes.Contains(authorizationScheme);
    }
  }
}
