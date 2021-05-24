using System;
using Infra.DomainDrivenDesign.Base;
using MediatR;

namespace Troupon.Checkout.Core.Application.Events
{
  public class PaymentReceivedEvent : INotification,
    IDomainEvent
  {
    public PaymentReceivedEvent(
      Guid orderId)
    {
      OrderId = orderId;
      CreationDate = DateTime.UtcNow;
    }

    public Guid OrderId { get; set; }
    public DateTime CreationDate { get; set; }
  }
}
