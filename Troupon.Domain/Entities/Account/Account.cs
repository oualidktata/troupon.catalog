using Infra.DomainDrivenDesign.Base;
using Troupon.Catalog.Core.Domain.Entities.Common;
using Troupon.Catalog.Core.Domain.Entities.Merchant;

namespace Troupon.Catalog.Core.Domain.Entities.Account
{
    public class AccountId : EntityId
    {
        
    }
    
    public class Account : AggregateRoot<AccountId>
    {
        public MerchantId MerchantId { get; private set; }
        public string Name { get; private set; }
        public BillingInfo BillingInfo { get; private set; }
        public Location Location { get; private set; }
    }
}
