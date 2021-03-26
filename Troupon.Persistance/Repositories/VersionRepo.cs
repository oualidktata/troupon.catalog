using Microsoft.EntityFrameworkCore;
using Troupon.Catalog.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Troupon.Catalog.Infra.Persistence.Repositories
{
    public class DealRepo : IDealRepo
    {
        private readonly IDbContextFactory<CatalogDbContext> _factory;
        public DealRepo(IDbContextFactory<CatalogDbContext>  factory)
        {
             _factory = factory;
        }
        public DealEntity AddDeal(DealEntity Deal)
        {
            var dbContext = _factory.CreateDbContext();
            var added = dbContext.Deals.Add(Deal);
            dbContext.SaveChangesAsync();
            return added.Entity;
        }
        public List<DealEntity> GetDeals()
        {
            var dbContext = _factory.CreateDbContext();
            return dbContext.Deals.ToList();
        }
        public DealEntity GetDeal(Guid id)
        {
            var dbContext = _factory.CreateDbContext();
            return dbContext.Deals.FirstOrDefault(x => x.Id == id);
        }
    }
}
