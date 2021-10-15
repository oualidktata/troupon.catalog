using System;
using System.Linq;
using Infra.oAuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

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
    private const int SchemePart = 0;
    private const int TokenPart = 1;
    private static readonly string[] SupportedAuthorizationSchemes = { "bearer", "ssws" };

    private IHttpContextAccessor HttpContextAccessor { get; }

    private IOAuthSettings OAuthSettings { get; }

    public JwtIntrospector(IHttpContextAccessor httpContextAccessor, IOAuthSettings oAuthSettings)
    {
      HttpContextAccessor = httpContextAccessor;
      OAuthSettings = oAuthSettings;
    }

    public JwtIntrospection GetJwtIntrospection()
    {
      var authorizationHeader = GetAuthorizationHeader();
      if (!AuthorizationExists(authorizationHeader))
      {
        throw JwtIntrospectionException.AuthorizationHeaderMissing();
      }

      string accessToken = GetAccessToken(authorizationHeader);
      return new JwtIntrospection(accessToken, OAuthSettings.ClientId);
    }

    private string GetAuthorizationHeader()
    {
      if (HttpContextAccessor.HttpContext != null)
      {
        if (HttpContextAccessor.HttpContext.Request.Headers.TryGetValue(HeaderNames.Authorization, out Microsoft.Extensions.Primitives.StringValues value))
        {
          return value.ToString();
        }
      }

      return string.Empty;
    }

    private static bool AuthorizationExists(string authorizationHeader)
    {
      return string.IsNullOrEmpty(authorizationHeader);
    }

    private static string GetAccessToken(string authorizationHeader)
    {
      var authorizationScheme = GetAuthorizationScheme(authorizationHeader);
      if (!SupportedAuthorizationScheme(authorizationScheme))
      {
        throw JwtIntrospectionException.UnsupportedAuthorizationSchemes(SupportedAuthorizationSchemes);
      }

      return GetAuthorizationAccessToken(authorizationHeader);
    }

    private static string GetAuthorizationScheme(string authorizationHeader)
    {
      return GetAuthorizationHeaderPart(authorizationHeader, SchemePart);
    }

    private static string GetAuthorizationAccessToken(string authorizationHeader)
    {
      return GetAuthorizationHeaderPart(authorizationHeader, TokenPart);
    }

    private static string GetAuthorizationHeaderPart(string authorizationHeader, int index)
    {
      var authorizationHeaderParts = authorizationHeader.Split(' ');
      return authorizationHeaderParts[index].ToLower();
    }

    private static bool SupportedAuthorizationScheme(string authorizationScheme)
    {
      return SupportedAuthorizationSchemes.Contains(authorizationScheme);
    }
  }
}
