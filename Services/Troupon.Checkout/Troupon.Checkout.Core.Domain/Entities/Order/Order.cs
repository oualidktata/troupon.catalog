using System;
using System.Collections.Generic;
using Infra.DomainDrivenDesign.Base;
using Troupon.Checkout.Core.Domain.Entities.Common;
using Troupon.Checkout.Core.Domain.Enums;

namespace Troupon.Checkout.Core.Domain.Entities.Order
{
  public class Order : AggregateRoot
  {
    public DateTime Date { get; private set; }
    public virtual Address ShippingAddress { get; private set; }
    public virtual Customer.Customer Customer { get; private set; }
    public OrderStatus Status { get; private set; }
    public virtual ICollection<OrderItem> OrderItems { get; private set; }
    public Guid DealId { get; private set; }

    public Order(
      Guid dealId) : this()
    {
      Date = DateTime.UtcNow;
      Status = OrderStatus.New;
      DealId = dealId;
    }

    public Order()
    {
      OrderItems = new List<OrderItem>();
    }

    public void SetShippingAddress(
      Address address)
    {
      ShippingAddress = address;
    }

    public void AddOrderItem(
      string productName,
      Guid optionId,
      Price price,
      int quantity)
    {
      OrderItems.Add(
        new OrderItem(
          productName,
          optionId,
          price,
          quantity));
    }

    public void Validated()
    {
      Status = OrderStatus.PendingPayment;
    }

    public void PaymentReceived()
    {
      Status = OrderStatus.PaymentApproved;
    }

    public void PaymentRejected()
    {
      Status = OrderStatus.PaymentRejected;
    }

    public void ApproveExecution()
    {
      Status = OrderStatus.ExecutionApproved;
    }
  }
}
