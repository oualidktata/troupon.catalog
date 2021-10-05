using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Troupon.Catalog.Core.Domain.Dtos;
using Troupon.DealManagement.Core.Application.Commands;

namespace Troupon.Catalog.Api.Controllers
{
  [ApiController]
  [Route("api/v{version:apiVersion}/[controller]")]
  public class SimulationController : BaseController
  {
    public SimulationController(
      IMapper mapper,
      IMediator mediator)
        : base(
      mediator,
      mapper)
    {
    }

    /// <summary>
    /// Simulate the handling of the DealPublishedEvent in order to create a deal view in the catalog.
    /// </summary>
    /// <param name="model">model.</param>
    /// <param name="cancellationToken">cancellation token.</param>
    /// <returns>Returns the Deal created.</returns>
    /// <response code="201">Returned if the Deal view was Created.</response>
    /// <response code="400">Returned if the model couldn't be parsed or the customer couldn't be found.</response>
    /// <response code="406">Returned if no response found with an acceptable format.</response>
    /// <response code="422">Returned when the validation failed.</response>
    [ProducesResponseType(
      typeof(DealDto),
      StatusCodes.Status201Created)]
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
    [HttpPost]
    [Route("HandleDealEventPublished")] // hello ? HEEEEEELLLLLOOOO !
    [ApiVersion("1.0")]
    public async Task<ActionResult<DealDto>> Post(
      [FromBody] UpdateDealViewCommand model,
      CancellationToken cancellationToken)
    {
      try
      {
        var result = await Mediator.Send<DealDto>(
          model,
          cancellationToken);

        // await Mediator.Publish<DealCreatedEvent>(new DealCreatedEvent());
        // await DomainEvents.Raise(new DealCreatedEvent());
        var idResult = new { id = result.Id };
        return CreatedAtAction(
            nameof(CatalogController.Get),
            nameof(CatalogController),
            idResult,
            result);
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
