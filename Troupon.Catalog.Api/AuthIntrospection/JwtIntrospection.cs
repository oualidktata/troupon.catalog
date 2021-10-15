using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.Json.Serialization;

namespace Troupon.Catalog.Api.AuthIntrospection
{
  /// <summary>
  /// Based on OAuth standard https://www.oauth.com/oauth2-servers/token-introspection-endpoint/.
  /// </summary>
  public class JwtIntrospection
  {
    private JwtSecurityToken Jwt { get; set; }

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
        Jwt = ParseToken(accessToken);
        Active = IsActive();
        Expiration = ExtractExpirationValue();
        Username = ExtractSubjectValue();
        Scopes = ExtractScopes();
      }
      catch (Exception)
      {
      }

      ClientId = clientId;
    }

    private bool IsActive()
    {
      return Jwt.ValidTo > DateTime.Now;
    }

    private static JwtSecurityToken ParseToken(string accessToken)
    {
      var jwtHandler = new JwtSecurityTokenHandler();
      var jwt = jwtHandler.ReadJwtToken(accessToken) as JwtSecurityToken;
      return jwt;
    }

    private string ExtractScopes()
    {
      var scopeClaims = Jwt.Claims.Where(c => c.Type == "scp");
      var scopes = string.Join(" ", scopeClaims.Select(sc => sc.Value));
      return scopes;
    }

    private string ExtractSubjectValue()
    {
      return ExtractClaimValue<string>("sub");
    }

    private int ExtractExpirationValue()
    {
      return ExtractClaimValue<int>("exp");
    }

    private T ExtractClaimValue<T>(string claimType)
    {
      var value = Jwt.Claims.Where(c => c.Type == claimType).First().Value;
      return (T)Convert.ChangeType(value, typeof(T));
    }
  }
}
