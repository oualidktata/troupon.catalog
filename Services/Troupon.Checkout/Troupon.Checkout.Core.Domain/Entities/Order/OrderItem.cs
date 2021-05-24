using System;
using Infra.DomainDrivenDesign.Base;
using Troupon.Checkout.Core.Domain.Entities.Common;

namespace Troupon.Checkout.Core.Domain.Entities.Order
{
  public class OrderItem : Entity
  {
    public string ProductName { get; private set; }
    public Guid DealOptionId { get; private set; }
    public virtual Price Price { get; private set; }
    public int Quantity { get; private set; }

    public OrderItem(
      string productName,
      Guid dealOptionId,
      Price price,
      int quantity)
    {
      ProductName = productName;
      DealOptionId = dealOptionId;
      Price = price;
      Quantity = quantity;
    }

    public OrderItem()
    {
    }
  }
}
