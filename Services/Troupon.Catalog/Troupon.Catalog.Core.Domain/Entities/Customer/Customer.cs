using Infra.DomainDrivenDesign.Base;

namespace Troupon.Catalog.Core.Domain.Entities.Customer
{
  public class Customer : AggregateRoot
  {
    public string Name { get; set; }
  }
}
