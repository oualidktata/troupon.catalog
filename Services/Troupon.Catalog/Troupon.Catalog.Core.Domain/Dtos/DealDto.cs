using System;
using Troupon.Catalog.Core.Domain.Enums;

namespace Troupon.Catalog.Core.Domain.Dtos
{
    public class DealDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Limitation { get; set; }
        public string OtherConditions { get; set; }
        public DealStatus Status { get; set; }
        public Guid AccountId { get; set; }
        public string MerchantName { get; set; }
    }
}
