using System;

namespace Troupon.Catalog.Core.Domain.Entities
{
    public class DealEntity : IAggregateRoot, IAuditable
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
       
        public string Description { get; set; }
        public string Details { get; set; }
        /// <summary>
        /// Id of the Merchant
        /// </summary>
        public Guid MerchantId { get; set; }
        public MerchantEntity Merchant { get; set; }
    }
}