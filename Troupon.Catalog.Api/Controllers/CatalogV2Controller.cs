using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Infra.Api;
using Infra.Api.Conventions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Troupon.Catalog.Core.Application.Queries.Deals;
using Troupon.Catalog.Core.Domain.Dtos;
using Troupon.Catalog.Core.Domain.InputModels;

namespace Troupon.Catalog.Api.Controllers
{
  [ApiController]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiVersion("2.0")]
  [ApiConventionType(typeof(PwcApiConventions))]
  public class CatalogV2Controller : PwcBaseController
  {
    public CatalogV2Controller(IMapper mapper, IMediator mediator)
        : base(mediator, mapper)
    {
    }

    [SwaggerOperation(
      Description = "Returns all active Deals",
      OperationId = "SearchDeals",
      Tags = new[] { "Search" })]
    [HttpPost("search")]
    public async Task<ActionResult<IEnumerable<DealDto>>> Search([FromBody] SearchDealsFilter filter, CancellationToken cancellationToken)
    {
      try
      {
        if (!ModelState.IsValid)
        {
          return BadRequest(new ValidationProblemDetails());
        }

        var result = await Mediator.Send(new GetDealsQuery(filter), cancellationToken);

        return Ok(result);
      }
      catch (Exception exception)
      {
        var result = StatusCode(StatusCodes.Status500InternalServerError, exception);
        return await Task.FromResult(result);
      }
    }
  }
}
