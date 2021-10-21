using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Troupon.Catalog.Api.Authentication
{
  public static class HeaderUtils
  {
    public static string GetRequestAuthorizationHeader(this HttpContext httpContext)
    {
      return GetRequestAuthorizationHeader(httpContext.Request);
    }

    public static string GetRequestAuthorizationHeader(this HttpRequest httpRequest)
    {
      if (httpRequest.Headers.TryGetValue(HeaderNames.Authorization, out Microsoft.Extensions.Primitives.StringValues value))
      {
        return value.ToString();
      }

      return string.Empty;
    }
  }
}
