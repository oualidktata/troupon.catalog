using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Troupon.Checkout.Core.Application.Commands;
using Troupon.Checkout.Core.Application.Events;

namespace Troupon.Checkout.Core.Application.Handlers.Events
{
  public class OrderValidatedEventHandler : INotificationHandler<OrderValidatedEvent>
  {
    private readonly IMediator _mediator;

    public OrderValidatedEventHandler(
      IMediator mediator)
    {
      _mediator = mediator;
    }

    public async Task Handle(
      OrderValidatedEvent notification,
      CancellationToken cancellationToken)
    {
      await _mediator.Send(
        new ProcessPaymentCommand(notification.OrderId),
        cancellationToken);
    }
  }
}
