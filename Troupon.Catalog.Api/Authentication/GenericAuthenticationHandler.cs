using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Infra.oAuthService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Troupon.Catalog.Api.Authentication
{
  public class GenericAuthenticationHandler : AuthenticationHandler<TokenAuthenticationOptions>
  {
    private const string SECURITYKEY = "key here";

    public GenericAuthenticationHandler(
      IOptionsMonitor<TokenAuthenticationOptions> options,
      ILoggerFactory logger,
      UrlEncoder encoder,
      ISystemClock clock)
      : base(options, logger, encoder, clock)
    {
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
      return await GenerateAuthenticationResult(GetToken());
    }

    private async Task<AuthenticateResult> GenerateAuthenticationResult(string token)
    {
      try
      {
        var success = Authenticate(token);
        return AuthenticateResult.Success(success);
      }

      // TODO HANDLE DOMAIN EXCEPTIONS
      catch (Exception ex)
      {
        return AuthenticateResult.Fail(ex.Message);
      }
    }

    private AuthenticationTicket Authenticate(string token)
    {
      var jwt = ValidateToken(token);

      var claims = GetClaims(jwt);
      ValidateClaims(claims);

      return GenerateAuthenticationTicket(claims);
    }

    private void ValidateClaims(Claim[] claims)
    {
      // SOME LOGIC TO SAY
      // OK IT'S GOOD TO GO
    }

    private JwtSecurityToken ValidateToken(string token)
    {
      if (string.IsNullOrEmpty(token))
      {
        throw new Exception("The provided token is null");
      }

      var key = GetSecurityKey();
      var validationParameters = SetupValidationParameters(key);

      if (!TryValidateToken(token, validationParameters, out JwtSecurityToken? jwtSecurityToken))
      {
        throw new Exception($"Could not validate the token");
      }

      return jwtSecurityToken!;
    }

    private string GetToken()
    {
      var authorizationHeader = Request.GetRequestAuthorizationHeader();
      var scheme = ExtractAuthorizationScheme(authorizationHeader);
      var token = ExtractAuthorizationToken(authorizationHeader, scheme);
      return token;
    }

    private string ExtractAuthorizationScheme(string authorizationHeader)
    {
      var scheme = authorizationHeader.Split(' ')[0];
      return scheme;
    }

    private string ExtractAuthorizationToken(string authorizationHeader, string authorizationScheme)
    {
      return authorizationHeader.Substring(authorizationScheme.Length).Trim();
    }

    private AuthenticationTicket GenerateAuthenticationTicket(Claim[] claims)
    {
      var identity = new ClaimsIdentity(claims);
      var principal = new ClaimsPrincipal(identity);
      var properties = new AuthenticationProperties();
      return new AuthenticationTicket(principal, properties, Scheme.Name);
    }

    private static Claim[] GetClaims(JwtSecurityToken jwt)
    {
      /*

      // Create Identity
      var claims = new[]
      {
          new Claim(
            "token",
            token),
          new Claim(
            "TenantId",
            "pwc"),

          // new Claim(
          //  ClaimTypes.Role,
          //  "admin"),//consult DB to get Role claims and add them to the identity
          // new Claim(ClaimTypes.NameIdentifier,new Guid().ToString())
        };

      return claims;3
      */

      return jwt.Claims.ToArray();
    }

    private TokenValidationParameters SetupValidationParameters(SymmetricSecurityKey key)
    {
      return new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuers = Options.ValidIssuers,
        ValidAudiences = Options.ValidAudiences,
        IssuerSigningKey = key,
      };
    }

    private static SymmetricSecurityKey GetSecurityKey()
    {
      return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SECURITYKEY));
    }

    private static bool TryValidateToken(string token, TokenValidationParameters validationParameters, out JwtSecurityToken? validatedJwt)
    {
      validatedJwt = null;
      try
      {
        new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out SecurityToken securityToken);
        validatedJwt = (JwtSecurityToken)securityToken;

        return true;
      }
      catch
      {
        return false;
      }
    }
  }
}
