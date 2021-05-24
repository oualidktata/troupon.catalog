using System;
using Troupon.Checkout.Core.Domain.Enums;

namespace Troupon.Checkout.Core.Domain.Dtos
{
  public class OrderDto
  {
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public OrderStatus Status { get; set; }
  }
}
