using System;
using MediatR;
using Troupon.Checkout.Core.Domain.Dtos;

namespace Troupon.Checkout.Core.Application.Commands
{
  public class PlaceOrderCommand : IRequest<OrderDto>
  {
    public Guid DealId { get; set; }
    public Guid DealOptionId { get; set; }

    public string StreetNumber { get; set; }
    public string StreetLine1 { get; set; }
    public string StreetLine2 { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }
    public string StateProvince { get; set; }
  }
}
