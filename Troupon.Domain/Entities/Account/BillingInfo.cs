using Infra.DomainDrivenDesign.Base;
using Troupon.Catalog.Core.Domain.Entities.Common;

namespace Troupon.Catalog.Core.Domain.Entities.Account
{
    public class BillingInfoId : EntityId
    {
        
    }
    
    public class BillingInfo : Entity<BillingInfoId>
    {
        public CreditCard CreditCard { get; private set; }
        public Address Address { get; private set; }
    }
}
