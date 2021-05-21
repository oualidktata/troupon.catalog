using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Troupon.Catalog.Core.Application.Commands;
using Troupon.Catalog.Core.Application.Events;

namespace Troupon.Catalog.Core.Application.Handlers.Commands
{
  public class ProcessPaymentCommandHandler : IRequestHandler<ProcessPaymentCommand, Unit>
  {
    public async Task<Unit> Handle(
      ProcessPaymentCommand request,
      CancellationToken cancellationToken)
    {
      // Process Payment

      await DomainEvents.Raise(
        new PaymentReceivedEvent(request.OrderId));

      return await Task.FromResult(Unit.Value);
    }
  }
}
