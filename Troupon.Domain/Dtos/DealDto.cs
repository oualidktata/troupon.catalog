using System;

namespace Troupon.Catalog.Core.Domain.Dtos
{
    public class DealDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public string Details { get; set; }
        /// <summary>
        /// unique identifier of the Merchant
        /// </summary>
        public Guid MerchantId { get; set; }
        public string MerchantName { get; set; }
        //adress
    }
}
