using AutoMapper;
using Troupon.Catalog.Core.Domain.Dtos;
using Troupon.Catalog.Core.Domain.Entities.Deal;

namespace Troupon.Catalog.Infra.Persistence
{
  public class AutomapperProfile : Profile
  {
    public AutomapperProfile()
    {
      CreateMap<DealView, DealDetailsDto>();
    }
  }
}
