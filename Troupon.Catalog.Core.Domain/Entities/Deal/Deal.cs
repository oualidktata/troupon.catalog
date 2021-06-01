using System;
using Infra.DomainDrivenDesign.Base;
using Troupon.Catalog.Core.Domain.Enums;

namespace Troupon.Catalog.Core.Domain.Entities.Deal
{
  public class Deal : AggregateRoot
  {
    public string Description { get; private set; }
    public string Title { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public int Limitation { get; private set; }
    public string OtherConditions { get; private set; }
    public DealStatus Status { get; private set; }
    public float Price { get; private set; }

    public Deal()
    {

    }
  }
}
