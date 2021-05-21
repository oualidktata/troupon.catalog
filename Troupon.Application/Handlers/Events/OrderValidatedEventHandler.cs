using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Troupon.Catalog.Core.Application.Commands;
using Troupon.Catalog.Core.Application.Events;

namespace Troupon.Catalog.Core.Application.Handlers.Events
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
