using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Infra.oAuthService;
using Microsoft.AspNetCore.Authentication;
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
      return await GenerateAuthenticationResult(GetToken());
    }

    private async Task<AuthenticateResult> GenerateAuthenticationResult(string token)
    {
      try
      {
        var success = await Authenticate(token);
        return AuthenticateResult.Success(success);
      }

      // TODO HANDLE DOMAIN EXCEPTIONS
      catch (Exception ex)
      {
        return AuthenticateResult.Fail(ex.Message);
      }
    }

    private async Task<AuthenticationTicket> Authenticate(string token)
    {
      var jwt = await ValidateToken(token);
      DefaultAuthorizationFromClaims(jwt.Claims);
      return GenerateAuthenticationTicket(jwt.Claims);
    }

    private void DefaultAuthorizationFromClaims(IEnumerable<Claim> claims)
    {
      var scopes = claims.Where(c => c.Type == "scp");

      // TODO: EXAMPLE ONLY
      // Should be changed to be configurable
      if (!scopes.Any(s => s.Value == "admin"))
      {
        throw new Exception("Need \"admin\" scope to be authorized");
      }

      if (!scopes.Any(s => s.Value == "openid"))
      {
        throw new Exception("Need \"openid\" scope to be authorized");
      }
    }

    private async Task<JwtSecurityToken> ValidateToken(string token)
    {
      if (string.IsNullOrEmpty(token))
      {
        throw new Exception("The provided token is null");
      }

      var validationParameters = await SetupValidationParameters();
      return ValidationWithParameters(token, validationParameters);
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

    private AuthenticationTicket GenerateAuthenticationTicket(IEnumerable<Claim> claims)
    {
      var identity = new ClaimsIdentity(claims);
      var principal = new ClaimsPrincipal(identity);
      var properties = new AuthenticationProperties();
      return new AuthenticationTicket(principal, properties, Scheme.Name);
    }

    // https://developer.okta.com/code/dotnet/jwt-validation/#validate-a-token
    private async Task<TokenValidationParameters> SetupValidationParameters()
    {
      var configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
          Options.ValidIssuer + "/.well-known/oauth-authorization-server",
          new OpenIdConnectConfigurationRetriever(),
          new HttpDocumentRetriever());

      var discoveryDocument = await configurationManager.GetConfigurationAsync();
      var signingKeys = discoveryDocument.SigningKeys;

      var validationParameters = new TokenValidationParameters
      {
        // Clock skew compensates for server time drift.
        // We recommend 5 minutes or less:
        ClockSkew = TimeSpan.FromMinutes(5),

        // Specify the key used to sign the token:
        ValidateIssuerSigningKey = true,
        IssuerSigningKeys = signingKeys,
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

      return validationParameters;
    }

    private static JwtSecurityToken ValidationWithParameters(string token, TokenValidationParameters validationParameters)
    {
      var handler = new JwtSecurityTokenHandler();
      handler.ValidateToken(token, validationParameters, out SecurityToken securityToken);
      var validatedJwt = (JwtSecurityToken)securityToken;

      // Okta uses RS256
      var expectedAlg = SecurityAlgorithms.RsaSha256;
      if (validatedJwt.Header?.Alg == null || validatedJwt.Header?.Alg != expectedAlg)
      {
        throw new SecurityTokenValidationException("The alg must be RS256.");
      }

      return validatedJwt;
    }
  }
}
