using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Infra.oAuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using Troupon.Catalog.Api.Conventions;

namespace Portal.Api.Controllers
{
  [ApiController]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiVersion("1.0")]
  [ApiConventionType(typeof(PwcApiConventions))]
  public class OAuthController : ControllerBase
  {
    private IAuthService TokenService { get; }

    private IOAuthSettings OAuthSettings { get; }

    public OAuthController(IAuthService tokenService, IOAuthSettings oAuthSettings)
    {
      TokenService = tokenService;
      OAuthSettings = oAuthSettings;
    }

    [SwaggerOperation(
       Description = "Authenticate the API",
       OperationId = "GetAccessToken")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("token")]
    public async Task<IActionResult> GetToken()
    {
      try
      {
        var token = await TokenService.GetToken();
        if (token == null)
        {
          return await Task.FromResult(StatusCode(StatusCodes.Status500InternalServerError, "Token is empty"));
        }

        return Ok(token);
      }
      catch (Exception ex)
      {
        return await Task.FromResult(StatusCode(StatusCodes.Status500InternalServerError, ex.Message));
      }
    }

    [SwaggerOperation(
       Description = "Get the information about the current token used fo autorization",
       OperationId = "GetAccessTokenIntrospection")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("token/introspect")]
    public async Task<IActionResult> Introspect()
    {
      try
      {
        if (HttpContext.Request.Headers.TryGetValue(HeaderNames.Authorization, out Microsoft.Extensions.Primitives.StringValues value))
        {
          var authorizationHeaderParts = value.ToString().Split(' ');
          if (authorizationHeaderParts.First().ToLower() == "bearer")
          {
            var accessToken = authorizationHeaderParts.Skip(1).First();

            var jwtHandler = new JwtSecurityTokenHandler();
            var jwt = jwtHandler.ReadJwtToken(accessToken) as JwtSecurityToken;

            var expirationClaim = jwt.Claims.Where(c => c.Type == "exp").First();
            var scopeClaim = jwt.Claims.Where(c => c.Type == "scp").First();
            var subjectClaim = jwt.Claims.Where(c => c.Type == "sub").First();

            // https://www.oauth.com/oauth2-servers/token-introspection-endpoint/
            var introspectionResponse = new
            {
              active = jwt.ValidTo > DateTime.Now,
              scope = scopeClaim.Value,
              client_id = OAuthSettings.ClientId,
              username = subjectClaim.Value,
              exp = expirationClaim.Value,
            };

            return Ok(introspectionResponse);
          }
        }

        return await Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, "bearer token not found"));
      }
      catch (Exception ex)
      {
        return await Task.FromResult(StatusCode(StatusCodes.Status500InternalServerError, ex.Message));
      }
    }
  }
}
