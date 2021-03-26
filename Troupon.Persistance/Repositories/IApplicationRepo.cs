using Troupon.Catalog.Core.Domain.Dtos;
using System;
using System.Collections.Generic;

namespace Troupon.Catalog.Infra.Persistence.Repositories
{
    public interface IApplicationRepo
    {
        IList<ApplicationDto> GetApplications();
        ApplicationDto GetFirstApplication(Guid id);
    }
}