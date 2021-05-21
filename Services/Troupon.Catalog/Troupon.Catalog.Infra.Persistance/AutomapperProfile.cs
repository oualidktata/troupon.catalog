using AutoMapper;
using Troupon.Catalog.Core.Domain.Dtos;
using Troupon.Catalog.Core.Domain.Entities.Deal;
using Troupon.Catalog.Core.Domain.Entities.Merchant;
using Troupon.Catalog.Core.Domain.Entities.Order;

namespace Troupon.Catalog.Infra.Persistence
{
  public class AutomapperProfile : Profile
  {
    public AutomapperProfile()
    {
      CreateMap<Deal, DealDto>()
        .ForMember(
          x => x.MerchantName,
          opt => opt.MapFrom(src => src.Account.Merchant.Name));
      CreateMap<Merchant, MerchantDto>();
      CreateMap<Order, OrderDto>();
    }
  }
}
