using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Troupon.Checkout.Core.Application.Commands;
using Troupon.Checkout.Core.Domain.Dtos;

namespace Troupon.Checkout.Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  [Produces(
    "application/json",
    "application/xml")]
  [Consumes(
    "application/json",
    "application/xml")]
  public class OrderController : BaseController
  {
    public OrderController(
      IMediator mediator,
      IMapper mapper) : base(
      mediator,
      mapper)
    {
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> Post(
      [FromBody] PlaceOrderCommand model,
      CancellationToken cancellationToken)
    {
      try
      {
        var result = await Mediator.Send<OrderDto>(
          model,
          cancellationToken);

        return CreatedAtAction(
          nameof(Post),
          new {id = result.Id},
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
