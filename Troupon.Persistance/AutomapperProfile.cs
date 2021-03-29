using AutoMapper;
using Troupon.Catalog.Core.Domain.Dtos;
using Troupon.Catalog.Core.Domain.Entities;
namespace Troupon.Catalog.Infra.Persistence
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            //Producer
            CreateMap<DealEntity, DealDto>()
                .ForMember(x=>x.MerchantName,opt=>opt.MapFrom(src=>src.Merchant.Name));
            CreateMap<MerchantEntity, MerchantDto>()
                .ForMember(x => x.NumberOfDeals, opt => opt.MapFrom(src => src.Deals.Count));
                
        }
    }
}
