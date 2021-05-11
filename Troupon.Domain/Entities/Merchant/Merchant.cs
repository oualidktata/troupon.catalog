using Infra.DomainDrivenDesign.Base;

namespace Troupon.Catalog.Core.Domain.Entities.Merchant
{
    public class MerchantId : EntityId
    {
        
    }
    
    public class Merchant : AggregateRoot<MerchantId>
    {
        public string Name { get; private set; }
        public string Website { get; private set; }
    }
}
