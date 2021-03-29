using System;
using System.Collections.Generic;

namespace Troupon.Catalog.Core.Domain.Entities
{
    public class MerchantEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUri { get; set; }
        public ICollection<DealEntity> Deals { get; set; }
    }
}