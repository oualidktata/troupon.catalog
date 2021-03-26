using AutoMapper;
using Troupon.Catalog.Core.Application.Commands;
using Troupon.Catalog.Core.Domain.Entities;

namespace Troupon.Catalog.Core.Application
{
    public class AutomapperProfileDomain : Profile
    {
        public AutomapperProfileDomain()
        {
           CreateMap<CreateDealCommand,DealEntity >();
           

        }
    }
}
