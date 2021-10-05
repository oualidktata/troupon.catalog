using System;
using System.Threading.Tasks;
using Infra.oAuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Portal.Api.Controllers
{
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiController]
  public class OAuthController : ControllerBase
  {
    private IAuthService TokenService { get; set; }

    public OAuthController(IAuthService tokenService)
    {
      TokenService = tokenService;
    }

    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    [SwaggerOperation(
       Description = "Authenticate the API",
       OperationId = "GetAccessToken",
       Tags = new[] { "*GetToken 1st*" })]
    [HttpPost("token")]
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    public async Task<IActionResult> Token()
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
  }
}
