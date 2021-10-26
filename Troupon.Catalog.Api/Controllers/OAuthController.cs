using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Infra.oAuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using Troupon.Catalog.Api.AuthIntrospection;
using Troupon.Catalog.Api.Authorization.Policies;
using Troupon.Catalog.Api.Conventions;

namespace Portal.Api.Controllers
{
  [ApiController]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiVersion("1.0")]
  [ApiConventionType(typeof(PwcApiConventions))]
  public class OAuthController : ControllerBase
  {
    private IM2MOAuthFlowService TokenService { get; }

    private IJwtIntrospector JwtIntrospector { get; }

    public OAuthController(IM2MOAuthFlowService tokenService, IJwtIntrospector jwtIntrospector)
    {
      TokenService = tokenService;
      JwtIntrospector = jwtIntrospector;
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
    [Authorize(Policy = AdminOnlyPolicy.Key)]
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
        var jwtIntrospection = JwtIntrospector.GetJwtIntrospection();
        return Ok(jwtIntrospection);
      }
      catch (JwtIntrospectionException ex)
      {
        return await Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, ex.Message));
      }
      catch (Exception ex)
      {
        return await Task.FromResult(StatusCode(StatusCodes.Status500InternalServerError, ex.Message));
      }
    }
  }
}
