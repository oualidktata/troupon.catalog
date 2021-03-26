using Troupon.Catalog.Infra.Persistence.Repositories;
using System;
using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using Troupon.Catalog.Core.Domain.Dtos;

namespace Troupon.Catalog.Service.Api.Schema
{
    //[ExtendObjectType(Name = "Query")]
    public class ApplicationQueries
    {
        private IApplicationRepo _repo;


    public ApplicationQueries([ScopedService] IApplicationRepo repo)
    {
        _repo = repo;
    }
    public ApplicationDto GetApplication(Guid appId)
    {
        return _repo.GetFirstApplication(appId);
    }
    public ApplicationDto GetOneApplication()
    {
        return new ApplicationDto { Name = "Sample App", Id = Guid.NewGuid(), Description = "description" };
    }

    ////[UseDbContext(typeof(CatalogDbContext))]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ApplicationDto> GetApplications()
    {
        //return new List<ApplicationDto>() { new ApplicationDto { Name = "Sample App 1", Id = Guid.NewGuid(), Description = "description" },
        //new ApplicationDto { Name = "Sample App 2", Id = Guid.NewGuid(), Description = "description" },
        //new ApplicationDto { Name = "Sample App 3", Id = Guid.NewGuid(), Description = "description" }}.AsQueryable();
        return _repo.GetApplications().AsQueryable();
    }
    }
}
