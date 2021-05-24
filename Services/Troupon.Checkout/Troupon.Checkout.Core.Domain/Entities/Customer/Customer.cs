using Infra.DomainDrivenDesign.Base;

namespace Troupon.Checkout.Core.Domain.Entities.Customer
{
  public class Customer : AggregateRoot
  {
    public string Name { get; set; }
  }
}
