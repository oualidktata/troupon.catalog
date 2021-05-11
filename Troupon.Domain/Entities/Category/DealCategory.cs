using Infra.DomainDrivenDesign.Base;

namespace Troupon.Catalog.Core.Domain.Entities.Category
{
    public class DealCategoryId : EntityId
    {
        
    }
    
    public class DealCategory : AggregateRoot<DealCategoryId>
    {
        public string Name { get; private set; }
        
    }
}
