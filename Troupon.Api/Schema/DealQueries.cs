using HotChocolate.Types;
using Troupon.Catalog.Infra.Persistence.Repositories;

namespace Troupon.Catalog.Service.Api.Schema
{
    [ExtendObjectType(Name = "Query")]
    public class DealQueries
    {
        private DealRepo _repo;

        public DealQueries(DealRepo repo)
        {
            _repo = repo;
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
