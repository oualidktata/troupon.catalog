using System;
using System.Collections.Generic;

namespace Troupon.Catalog.Core.Domain.Dtos
{
    public class MerchantDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUri { get; set; }
        public ICollection<DealDto> Deals { get; set; }
        public int NumberOfDeals { get; set; }
    }
}
