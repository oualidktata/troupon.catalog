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
using Troupon.Catalog.Core.Application.Queries.Deals;
using Troupon.Catalog.Core.Domain.Dtos;
using Troupon.Catalog.Core.Domain.InputModels;

namespace Troupon.Catalog.Api.Controllers
{
  [ApiController]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ApiVersion("1.0")]
  [ApiVersion("2.0")]
  public class CatalogController : BaseController
  {
    public CatalogController(IMapper mapper, IMediator mediator)
      : base(mediator, mapper)
    {
    }

    /// <summary>
    /// Gets all active Deals.
    /// </summary>
    /// <param name="filter">filter.</param>
    /// <param name="cancellationToken">cancellationToken.</param>
    /// <returns>List of Deal Dtos.</returns>
    [ProducesDefaultResponseType]
    [SwaggerOperation(
      Description = "Returns all active Deals",
      OperationId = "SearchDeals",
      Tags = new[] { "Search" })]
    [HttpPost("search")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Authorize(Policy = "tenant-policy")]
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

    [ProducesResponseType(
      typeof(DealDto),
      StatusCodes.Status200OK)]
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
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [SwaggerOperation(
      Description = "Returns the Deal specified by Id",
      OperationId = "GetOneDeal",
      Tags = new[] { "One Deal" })]
    [HttpGet]
    [Route("{id}")]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
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
