using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Infra.oAuthService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Troupon.Catalog.Api.Authentication
{
  public class GenericAuthenticationHandler : AuthenticationHandler<TokenAuthenticationOptions>
  {
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
      if (AllowAnonymous())
      {
        return AuthenticateResult.NoResult();
      }

      var token = Request.GetAuthorizationToken();
      return await GenerateAuthenticationResult(token);
    }

    private bool AllowAnonymous()
    {
      var endpoint = Context.GetEndpoint();
      return endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null;
    }

    private async Task<AuthenticateResult> GenerateAuthenticationResult(string token)
    {
      try
      {
        var success = await Authenticate(token);
        return AuthenticateResult.Success(success);
      }
      catch (SecurityTokenValidationException ex)
      {
        return AuthenticateResult.Fail(ex.Message);
      }
    }

    private async Task<AuthenticationTicket> Authenticate(string token)
    {
      var jwt = await ValidateToken(token);
      AuthorizeClaims(jwt.Claims);
      return GenerateAuthenticationTicket(jwt.Claims);
    }

    private async Task<JwtSecurityToken> ValidateToken(string token)
    {
      if (string.IsNullOrEmpty(token))
      {
        throw new SecurityTokenValidationException("Should provide token");
      }

      var validationParameters = await GetValidationParameters();
      return ValidationWithParameters(token, validationParameters);
    }

    private async Task<TokenValidationParameters> GetValidationParameters()
    {
      // https://developer.okta.com/code/dotnet/jwt-validation/#validate-a-token
      return new TokenValidationParameters
      {
        // Clock skew compensates for server time drift.
        // We recommend 5 minutes or less:
        ClockSkew = TimeSpan.FromMinutes(5),

        // Specify the key used to sign the token:
        ValidateIssuerSigningKey = true,
        IssuerSigningKeys = await GetSigningKeys(),
        RequireSignedTokens = true,

        // Ensure the token hasn't expired:
        RequireExpirationTime = true,
        ValidateLifetime = true,

        // Ensure the token audience matches our audience value (default true):
        ValidateAudience = true,
        ValidAudiences = Options.ValidAudiences,

        // Ensure the token was issued by a trusted authorization server (default true):
        ValidateIssuer = true,
        ValidIssuer = Options.ValidIssuer,
      };
    }

    private async Task<ICollection<SecurityKey>> GetSigningKeys()
    {
      var configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                Options.ValidIssuer + "/.well-known/oauth-authorization-server",
                new OpenIdConnectConfigurationRetriever(),
                new HttpDocumentRetriever());

      var discoveryDocument = await configurationManager.GetConfigurationAsync();
      return discoveryDocument.SigningKeys;
    }

    private static JwtSecurityToken ValidationWithParameters(string token, TokenValidationParameters validationParameters)
    {
      var validatedJwt = ValidateWithJwtSecurityTokenHandler(token, validationParameters);
      EnsureSignedWithSha256(validatedJwt);
      return validatedJwt;
    }

    private static JwtSecurityToken ValidateWithJwtSecurityTokenHandler(string token, TokenValidationParameters validationParameters)
    {
      var handler = new JwtSecurityTokenHandler();
      handler.ValidateToken(token, validationParameters, out SecurityToken securityToken);
      return (JwtSecurityToken)securityToken;
    }

    private static void EnsureSignedWithSha256(JwtSecurityToken validatedJwt)
    {
      // Okta uses RS256
      // https://developer.okta.com/code/dotnet/jwt-validation/#additional-validation-for-access-tokens
      var expectedAlg = SecurityAlgorithms.RsaSha256;
      if (validatedJwt.Header?.Alg == null || validatedJwt.Header?.Alg != expectedAlg)
      {
        throw new SecurityTokenValidationException("The alg must be RS256");
      }
    }

    private void AuthorizeClaims(IEnumerable<Claim> claims)
    {
      var scopes = claims.ExtractScopes();

      // TODO: EXAMPLE ONLY
      // Should be changed to be configurable
      if (!scopes.Contains("admin"))
      {
        throw new SecurityTokenValidationException("Need \"admin\" scope to be authorized");
      }

      if (!scopes.Contains("openid"))
      {
        throw new SecurityTokenValidationException("Need \"openid\" scope to be authorized");
      }
    }

    private AuthenticationTicket GenerateAuthenticationTicket(IEnumerable<Claim> claims)
    {
      var identity = new ClaimsIdentity(claims);
      var principal = new ClaimsPrincipal(identity);
      var properties = new AuthenticationProperties();
      return new AuthenticationTicket(principal, properties, Scheme.Name);
    }
  }
}
