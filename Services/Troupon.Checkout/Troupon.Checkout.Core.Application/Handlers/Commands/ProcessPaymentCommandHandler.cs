using System.Threading;
using System.Threading.Tasks;
using Infra.MediatR.Events;
using MediatR;
using Troupon.Checkout.Core.Application.Commands;
using Troupon.Checkout.Core.Application.Events;

namespace Troupon.Checkout.Core.Application.Handlers.Commands
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
