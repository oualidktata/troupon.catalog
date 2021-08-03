using System.Collections.Generic;
using Infra.DomainDrivenDesign.Base;
using Troupon.Catalog.Core.Domain.Entities.Common;

namespace Troupon.Catalog.Core.Domain.Entities.Deal
{
  public class DealPrice : ValueObject
  {
    public float Amount { get; private set; }
    public virtual Currency Currency { get; private set; }

    public DealPrice()
    {
    }

    public DealPrice(
      float amount,
      Currency currency)
    {
      Amount = amount;
      Currency = currency;
    }

    protected override IEnumerable<object> GetEqualityValues()
    {
      yield return Currency;
      yield return Amount;
    }
  }
}
