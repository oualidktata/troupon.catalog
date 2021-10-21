using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;
using Troupon.Catalog.Api.Authentication;

namespace Troupon.Catalog.Api.AuthIntrospection
{
  /// <summary>
  /// Based on OAuth standard https://www.oauth.com/oauth2-servers/token-introspection-endpoint/.
  /// </summary>
  public class JwtIntrospection
  {
    [JsonPropertyName("active")]
    public bool Active { get; }

    [JsonPropertyName("scopes")]
    public string Scopes { get; }

    [JsonPropertyName("client_id")]
    public string ClientId { get; }

    [JsonPropertyName("username")]
    public string Username { get; }

    [JsonPropertyName("exp")]
    public int Expiration { get; }

    public JwtIntrospection(string accessToken, string clientId)
    {
      try
      {
        var jwt = ParseToken(accessToken);
        Active = jwt.IsActive();
        Expiration = jwt.ExtractExpirationValue();
        Username = jwt.ExtractSubjectValue();
        Scopes = FormatScopes(jwt.Claims.ExtractScopes());
      }
      catch (Exception)
      {
      }

      ClientId = clientId;
    }

    private static JwtSecurityToken ParseToken(string accessToken)
    {
      var jwtHandler = new JwtSecurityTokenHandler();
      var jwt = jwtHandler.ReadJwtToken(accessToken) as JwtSecurityToken;
      return jwt;
    }

    private static string FormatScopes(IEnumerable<string> scopes)
    {
      return string.Join(" ", scopes);
    }
  }
}
