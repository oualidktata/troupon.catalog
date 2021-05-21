using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Troupon.Catalog.Core.Application.Commands;
using Troupon.Catalog.Core.Application.Events;

namespace Troupon.Catalog.Core.Application.Handlers.Events
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

            await _mediator.Send(new ValidateOrderCommand(notification.OrderId), cancellationToken);
        }
    }
}
