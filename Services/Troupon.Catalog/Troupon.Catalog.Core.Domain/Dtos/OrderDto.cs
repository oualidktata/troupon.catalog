using System;
using Troupon.Catalog.Core.Domain.Enums;

namespace Troupon.Catalog.Core.Domain.Dtos
{
  public class OrderDto
  {
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public OrderStatus Status { get; set; }
  }
}
