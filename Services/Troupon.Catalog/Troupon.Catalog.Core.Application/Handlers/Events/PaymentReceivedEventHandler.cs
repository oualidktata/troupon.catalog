using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infra.Persistence.Repositories;
using MediatR;
using Troupon.Catalog.Core.Application.Events;
using Troupon.Catalog.Core.Domain.Entities.Order;

namespace Troupon.Catalog.Core.Application.Handlers.Events
{
  public class PaymentReceivedEventHandler : INotificationHandler<PaymentReceivedEvent>
  {
    private readonly IReadRepository<Order> _orderReadRepo;
    private readonly IWriteRepository<Order> _orderWriteRepo;

    public PaymentReceivedEventHandler(
      IReadRepository<Order> orderReadRepo,
      IWriteRepository<Order> orderWriteRepo)
    {
      _orderReadRepo = orderReadRepo;
      _orderWriteRepo = orderWriteRepo;
    }

    public Task Handle(
      PaymentReceivedEvent notification,
      CancellationToken cancellationToken)
    {
      // Do something

      var order = _orderReadRepo.Single(x => x.Id == notification.OrderId);
      order.PaymentReceived();

      _orderWriteRepo.Update(order);

      return Task.CompletedTask;
    }
  }
}
