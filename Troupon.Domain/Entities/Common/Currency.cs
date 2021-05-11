using System.Collections.Generic;
using Infra.DomainDrivenDesign.Base;

namespace Troupon.Catalog.Core.Domain.Entities.Common
{
    public class Currency : ValueObject
    {
        public string CurrencyName { get; private set; }

        public Currency(string currencyName)
        {
            CurrencyName = currencyName;
        }
        
        protected override IEnumerable<object> GetEqualityValues()
        {
            yield return CurrencyName;
        }
    }
}
