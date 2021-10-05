using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
  public class CatalogV2Controller : BaseController
  {
    public CatalogV2Controller(IMapper mapper, IMediator mediator)
        : base(mediator, mapper)
    {
    }

    [ProducesResponseType(
      typeof(IEnumerable<DealDto>),
      StatusCodes.Status200OK)]

    // [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(
      typeof(ValidationProblemDetails),
      StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(
      typeof(ProblemDetails),
      StatusCodes.Status409Conflict)]
    [ProducesResponseType(
      typeof(ProblemDetails),
      StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
    [SwaggerOperation(
      Description = "Returns all active Deals",
      OperationId = "SearchDeals",
      Tags = new[] { "Search" })]
    [HttpPost]
    [Route("search")]
    /*[Authorize(Roles = "crm-api-backend")]*/
    public async Task<ActionResult<IEnumerable<DealDto>>> Search(
      [FromBody] SearchDealsFilter filter,
      CancellationToken cancellationToken)
    {
      try
      {
        if (!ModelState.IsValid)
        {
          return BadRequest(new ValidationProblemDetails());
        }

        var result = await Mediator.Send<IEnumerable<DealDto>>(
          new GetDealsQuery(filter),
          cancellationToken);

        return Ok(result);
      }
      catch (Exception exception)
      {
        return await Task.FromResult(
          StatusCode(
            StatusCodes.Status500InternalServerError,
            exception));
      }
    }
  }
}
