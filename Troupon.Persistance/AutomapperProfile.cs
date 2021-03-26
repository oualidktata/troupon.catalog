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
            CreateMap<DealEntity, DealDto>();
            //consumer
            // CreateMap<DealDto, VersionViewModel>();
            // CreateMap<DealDto, VersionViewModel>();
            CreateMap<Application, ApplicationDto>();

        }
    }
}
