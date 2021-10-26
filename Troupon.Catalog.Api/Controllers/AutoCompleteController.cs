using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Infra.Api.Conventions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Troupon.Catalog.Api.Controllers
{
  [ApiController]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiVersion("1.0")]
  [ApiConventionType(typeof(PwcApiConventions))]
  public class AutoCompleteController : PwcBaseController
  {
    public AutoCompleteController(IMediator mediator, IMapper mapper)
        : base(mediator, mapper)
    {
    }

    [SwaggerOperation(
      Description = "Returns the 5 closest location to value by name",
      OperationId = "SearchLocations")]
    [HttpGet("locations")]
    public IEnumerable<string> SearchLocations(string value)
    {
      return Enumerable.Repeat(value, 5);
    }

    [SwaggerOperation(
      Description = "Returns the 5 closest deals to value by name",
      OperationId = "SearchDeals")]
    [HttpGet("deals")]
    public IEnumerable<string> SearchDeals(string value)
    {
      return Enumerable.Repeat(value, 5);
    }
  }
}
