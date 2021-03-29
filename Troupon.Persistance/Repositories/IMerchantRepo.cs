using Troupon.Catalog.Core.Domain.Dtos;
using System;
using System.Collections.Generic;

namespace Troupon.Catalog.Infra.Persistence.Repositories
{
    public interface IMerchantRepo
    {
        IList<MerchantDto> GetMerchant();
        MerchantDto GetFirstMerchant(Guid id);
    }
}