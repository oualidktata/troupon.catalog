using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Troupon.Checkout.Core.Application.Commands;
using Troupon.Checkout.Core.Application.Events;

namespace Troupon.Checkout.Core.Application.Handlers.Events
{
  public class OrderPlacedEventHandler : INotificationHandler<OrderPlacedEvent>
  {
    private readonly IMediator _mediator;

    public OrderPlacedEventHandler(
      IMediator mediator)
    {
      _mediator = mediator;
    }

    public async Task Handle(
      OrderPlacedEvent notification,
      CancellationToken cancellationToken)
    {
      // Trigger Validation

      await _mediator.Send(
        new ValidateOrderCommand(notification.OrderId),
        cancellationToken);
    }
  }
}
