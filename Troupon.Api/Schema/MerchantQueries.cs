using Troupon.Catalog.Infra.Persistence.Repositories;
using System;
using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using Troupon.Catalog.Core.Domain.Dtos;

namespace Troupon.Catalog.Service.Api.Schema
{
    //[ExtendObjectType(Name = "Query")]
    public class MerchantQueries
    {
        private IMerchantRepo _repo;


    public MerchantQueries([ScopedService] IMerchantRepo repo)
    {
        _repo = repo;
    }
    public MerchantDto GetMerchant(Guid appId)
    {
        return _repo.GetFirstMerchant(appId);
    }
    public MerchantDto GetOneMerchant()
    {
        return new MerchantDto { Name = "Sample App", Id = Guid.NewGuid(), ImageUri = "description" };
    }

    ////[UseDbContext(typeof(CatalogDbContext))]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<MerchantDto> GetMerchants()
    {
        //return new List<MerchantDto>() { new MerchantDto { Name = "Sample App 1", Id = Guid.NewGuid(), Description = "description" },
        //new MerchantDto { Name = "Sample App 2", Id = Guid.NewGuid(), Description = "description" },
        //new MerchantDto { Name = "Sample App 3", Id = Guid.NewGuid(), Description = "description" }}.AsQueryable();
        return _repo.GetMerchant().AsQueryable();
    }
    }
}
