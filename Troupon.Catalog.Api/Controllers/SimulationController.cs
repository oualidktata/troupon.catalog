using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Troupon.Catalog.Api.Conventions;
using Troupon.Catalog.Core.Domain.Dtos;
using Troupon.DealManagement.Core.Application.Commands;

namespace Troupon.Catalog.Api.Controllers
{
  [ApiController]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiVersion("1.0")]
  [ApiConventionType(typeof(PwcApiConventions))]
  public class SimulationController : BaseController
  {
    public SimulationController(IMapper mapper, IMediator mediator)
        : base(mediator, mapper)
    {
    }

    // [ApiExplorerSettings(IgnoreApi = true)]
    // [HttpPost("HandleDealEventPublished")]
    // public async Task<ActionResult<DealDetailsDto>> Post([FromBody] UpdateDealViewCommand model, CancellationToken cancellationToken)
    // {
    //  try
    //  {
    //    var result = await Mediator.Send(model, cancellationToken);

    // await Mediator.Publish<DealCreatedEvent>(new DealCreatedEvent());
    //    await DomainEvents.Raise(new DealCreatedEvent());
    //    var idResult = new { id = result.Id };
    //    return CreatedAtAction(
    //        nameof(CatalogController.Get),
    //        nameof(CatalogController),
    //        idResult,
    //        result);
    //  }
    //  catch (Exception exception)
    //  {
    //    return await Task.FromResult(
    //      StatusCode(
    //        StatusCodes.Status500InternalServerError,
    //        exception));
    //  }
    // }
  }
}
