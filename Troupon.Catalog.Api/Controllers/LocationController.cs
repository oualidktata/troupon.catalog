using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infra.Api;
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
  public class LocationController : PwcBaseController
  {
    public LocationController(IMediator mediator, IMapper mapper)
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
  }
}
