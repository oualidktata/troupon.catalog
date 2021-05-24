using AutoMapper;
using Troupon.Checkout.Core.Domain.Dtos;
using Troupon.Checkout.Core.Domain.Entities.Order;

namespace Troupon.Checkout.Infra.Persistence
{
  public class AutomapperProfile : Profile
  {
    public AutomapperProfile()
    {
      CreateMap<Order, OrderDto>();
    }
  }
}
