using Infra.DomainDrivenDesign.Base;
using Troupon.Catalog.Core.Domain.Entities.Common;

namespace Troupon.Catalog.Core.Domain.Entities.Account
{
  public class AccountId : EntityId
  {
  }

  /// <summary>
  /// Account: A merchant can have multiple accounts
  /// </summary>
  public class Account : AggregateRoot
  {
    public string Name { get; private set; }
    public virtual Merchant.Merchant Merchant { get; private set; }
    public virtual BillingInfo BillingInfo { get; private set; }
    public virtual Location Location { get; private set; }
  }
}
