using Troupon.Catalog.Core.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Troupon.Catalog.Infra.Persistence.Repositories
{
    public interface IDealRepo
    {
        DealEntity AddDeal(DealEntity Deal);
        DealEntity GetDeal(Guid id);
        List<DealEntity> GetDeals();
    }
}