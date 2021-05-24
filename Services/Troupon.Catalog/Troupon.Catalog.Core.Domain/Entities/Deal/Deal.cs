using System;
using System.Collections.Generic;
using System.Linq;
using Infra.DomainDrivenDesign.Base;
using Troupon.Catalog.Core.Domain.Entities.Category;
using Troupon.Catalog.Core.Domain.Entities.Common;
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
    public virtual Account.Account Account { get; private set; }
    public virtual ICollection<DealOption> Options { get; private set; }
    public virtual ICollection<DealCategory> Categories { get; private set; }

    public Deal()
    {
      Options = new List<DealOption>();
      Categories = new List<DealCategory>();
    }

    public DealOption GetOption(
      Guid optionId)
    {
      return Options.SingleOrDefault(x => x.Id == optionId);
    }

    public DealPrice GetOptionPrice(
      Guid optionId,
      Currency currency)
    {
      var option = GetOption(optionId);

      if (option == null) return null;

      return option.GetPrice(currency);
    }
  }
}
