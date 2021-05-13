using Infra.DomainDrivenDesign.Base;
using Troupon.Catalog.Core.Domain.Entities.Common;

namespace Troupon.Catalog.Core.Domain.Entities.Account
{
    public class BillingInfoId : EntityId
    {
        
    }
    
    public class BillingInfo : Entity
    {
        public virtual CreditCard CreditCard { get; private set; }
        public virtual Address Address { get; private set; }
    }
}
