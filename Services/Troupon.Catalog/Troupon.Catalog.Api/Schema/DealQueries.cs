using HotChocolate.Types;
using Infra.Persistence.Repositories;
using Troupon.Catalog.Core.Domain.Entities.Deal;

namespace Troupon.Catalog.Api.Schema
{
    [ExtendObjectType(Name = "Query")]
    public class DealQueries
    {
        private readonly IReadRepository<Deal> _dealsReadRepo;

        public DealQueries(
            IReadRepository<Deal> dealsReadRepo)
        {
            _dealsReadRepo = dealsReadRepo;
        }

        //public DealDto GetDeal(Guid DealId)
        //{
        //    return _repo.GetDeal(DealId);
        //}

        //[UsePaging]
        //[UseFiltering]
        //public List<DealDto> GetDeals()
        //{
        //    return _repo.GetDeals().;
        //}
    }
}
