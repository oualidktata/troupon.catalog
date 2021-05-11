using System.Collections.Generic;
using Infra.DomainDrivenDesign.Base;
using Troupon.Catalog.Core.Domain.Entities.Common;

namespace Troupon.Catalog.Core.Domain.Entities.Deal
{
    public class DealPrice : ValueObject
    {
        public Currency Currency { get; private set; }
        public Price OriginalPrice { get; private set; }
        public Price CurrentPrice { get; private set; }

        public DealPrice(Currency currency, Price originalPrice, Price currentPrice)
        {
            Currency = currency;
            OriginalPrice = originalPrice;
            CurrentPrice = currentPrice;
        }
        
        protected override IEnumerable<object> GetEqualityValues()
        {
            yield return Currency;
            yield return OriginalPrice;
            yield return CurrentPrice;
        }
    }
}
