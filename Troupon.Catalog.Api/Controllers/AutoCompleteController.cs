using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Troupon.Catalog.Api.Conventions;

namespace Troupon.Catalog.Api.Controllers
{
  [ApiController]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiVersion("1.0")]
  [ApiConventionType(typeof(PwcApiConventions))]
  public class AutoCompleteController : BaseController
  {
    public AutoCompleteController(IMediator mediator, IMapper mapper)
        : base(mediator, mapper)
    {
    }

    [SwaggerOperation(
     Description = "Returns the n closest location to value by name",
     OperationId = "locations")]
    [HttpGet]
    public Task<IEnumerable<string>> Get([FromQuery] string input)
    {
      return Task.FromResult(Enumerable.Repeat(input, 5));
    }

    [SwaggerOperation(
    Description = "Returns the n most recent searches",
    OperationId = "recent-search")]
    [HttpGet("recent-search")]
    public IEnumerable<string> SearchDeals(string value)
    {
      return Enumerable.Repeat(value, 5);
    }
  }
}
