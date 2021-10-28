using System;
using Infra.Api.Conventions;
using Microsoft.AspNetCore.Mvc;
using Troupon.Catalog.Core.Domain.Exceptions;

namespace Troupon.Catalog.Api.Controllers
{
  [ApiController]
  [ApiVersion("1.0")]
  [Route("api / v{version: apiVersion}/[controller]")]
  [ApiConventionType(typeof(PwcApiConventions))]
  public class ExceptionExampleController : Controller
  {
    public ExceptionExampleController()
    {
    }

    [HttpGet("generic-exception")]
    public IActionResult GenricException()
    {
      throw new Exception();
    }

    [HttpGet("domain-Exception")]
    public IActionResult DomainException()
    {
      throw new POCDomainException();
    }

    [HttpGet("validation-exception")]
    public IActionResult ValidationException()
    {
      // TODO: Change for ValidationException when implemented
      throw new POCDomainException();
    }
  }
}
