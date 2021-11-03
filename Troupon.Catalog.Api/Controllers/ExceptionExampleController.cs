using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Infra.Api;
using Infra.Api.Conventions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Troupon.Catalog.Core.Application.Queries.Deals;
using Troupon.Catalog.Core.Domain.Exceptions;
using Troupon.Catalog.Core.Domain.InputModels;

namespace Troupon.Catalog.Api.Controllers
{
  [ApiController]
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiConventionType(typeof(PwcApiConventions))]
  public class ExceptionExampleController : PwcBaseController
  {
    public ExceptionExampleController(IMapper mapper, IMediator mediator)
      : base(mediator, mapper)
    {
    }

    [HttpGet("generic-exception")]
    public IActionResult GenricException()
    {
      throw new Exception("This is an unknown generic exception");
    }

    [HttpGet("domain-Exception")]
    public IActionResult DomainException()
    {
      throw new DealDoesntExistException("Deal abc123 doesn't exist!");
    }

    [HttpPost("domain-Exception-MediatR")]
    public async Task<IActionResult> DomainExceptionMediatR([FromBody, BindRequired] SearchDealsFilter filter, CancellationToken cancellationToken)
    {
      var result = await Mediator.Send(new BrokenDealsQuery(filter), cancellationToken);
      return Ok(result);
    }

    [HttpGet("validation-exception")]
    public IActionResult ValidationException()
    {
      // TODO: Change for ValidationException when implemented
      throw new DealDoesntExistException("Deal abc123 doesn't exist!");
    }
  }
}
