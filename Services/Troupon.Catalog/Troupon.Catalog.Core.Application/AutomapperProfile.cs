using AutoMapper;
using Troupon.Catalog.Core.Application.Commands;
using Troupon.Catalog.Core.Domain.Entities.Deal;

namespace Troupon.Catalog.Core.Application
{
  public class AutomapperProfileDomain : Profile
  {
    public AutomapperProfileDomain()
    {
      CreateMap<CreateDealCommand, Deal>();
    }
  }
}
