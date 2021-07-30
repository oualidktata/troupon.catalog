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
using Troupon.Catalog.Core.Domain;
using Troupon.Catalog.Core.Domain.Dtos;
using Troupon.Catalog.Core.Domain.InputModels;

namespace Troupon.Catalog.Api.Controllers
{
  [ApiController]
  [Route("api/v{version:apiVersion}/[controller]")]
  [Produces(
    "application/json",
    "application/xml")]
  [Consumes(
    "application/json",
    "application/xml")]
  public class CatalogController : BaseController
  {
    public CatalogController(
      IMapper mapper,
      IMediator mediator) : base(
      mediator,
      mapper)
    {
    }

    /// <summary>
    /// Gets all active Deals
    /// </summary>
    /// <returns>List of Deal Dtos</returns>
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
      Tags = new[] { "Search" }
    )]
    [HttpPost]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("search")]
    [Authorize(Policy = "tenant-policy")]
    public async Task<ActionResult<IEnumerable<DealDto>>> Search(
      [FromBody,BindRequired] SearchDealsFilter filter,
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

    /// <summary>
    /// Gets a specific Deal
    /// </summary>
    /// <returns>Returns a Deal Dto</returns>
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
      Tags = new[] { "One Deal" }
    )]
    [HttpGet]
    [Route("{id}")]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    public async Task<ActionResult<IEnumerable<DealDto>>> Get(
      [BindRequired] Guid id,
      CancellationToken cancellationToken)
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
