using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using Troupon.Catalog.Api.Authorization.Policies;
using Troupon.Catalog.Api.Conventions;
using Troupon.Catalog.Core.Application.Queries.Deals;
using Troupon.Catalog.Core.Domain.Dtos;
using Troupon.Catalog.Core.Domain.InputModels;

namespace Troupon.Catalog.Api.Controllers
{
  [ApiController]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiConventionType(typeof(PwcApiConventions))]
  public class CatalogController : BaseController
  {
    public CatalogController(IMapper mapper, IMediator mediator)
      : base(mediator, mapper)
    {
    }

    [SwaggerOperation(
      Description = "Returns all active Deals",
      OperationId = "SearchDeals",
      Tags = new[] { "Search" })]
    [Authorize(Policy = TenantPolicy.Key)]
    [HttpPost("search")]
    public async Task<ActionResult<IEnumerable<SearchDealResponseDto>>> Search([FromBody, BindRequired] SearchDealsFilter filter, CancellationToken cancellationToken)
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
        var errorResult = StatusCode(StatusCodes.Status500InternalServerError, exception);
        return await Task.FromResult(errorResult);
      }
    }

    [SwaggerOperation(
      Description = "Returns the Deal specified by Id",
      OperationId = "GetOneDeal",
      Tags = new[] { "One Deal" })]
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<DealDto>>> Get([BindRequired] Guid id, CancellationToken cancellationToken)
    {
      try
      {
        var result = await Mediator.Send<DealDto>(
          new GetOneDealQuery() { Id = id },
          cancellationToken);
        if (result is null)
        {
          return NotFound(Result.Fail(new Error($"Could not find the Deal: {id}")));
        }

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
