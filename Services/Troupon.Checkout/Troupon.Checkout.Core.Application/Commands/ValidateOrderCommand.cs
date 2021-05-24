using System;
using MediatR;

namespace Troupon.Checkout.Core.Application.Commands
{
  public class ValidateOrderCommand : IRequest<bool>
  {
    public Guid OrderId { get; set; }

    public ValidateOrderCommand(
      Guid orderId)
    {
      OrderId = orderId;
    }
  }
}
