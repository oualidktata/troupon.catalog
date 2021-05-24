using System;
using MediatR;

namespace Troupon.Checkout.Core.Application.Commands
{
  public class ProcessPaymentCommand : IRequest<Unit>
  {
    public Guid OrderId { get; set; }

    public ProcessPaymentCommand(
      Guid orderId)
    {
      OrderId = orderId;
    }
  }
}
