using System;
using System.Collections.Generic;
using Infra.Common.Models;
using Infra.DomainDrivenDesign.Base;

namespace Troupon.Catalog.Core.Domain.Entities.Deal
{
  public class DealView:Entity,
        IRepoQueryable
  {
    public string Description { get; private set; }
    public string Title { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public int Limitation { get; private set; }
    public string OtherConditions { get; private set; }
    public string Status { get; private set; }
    public string MerchantName { get; private set; }
      public string[] Location { get; private set; }
        public string Account { get; private set; }
    public virtual ICollection<DealOption> Options { get; private set; }
    public virtual ICollection<string> Categories { get; private set; }
    public DealView()
    {
      Options = new List<DealOption>();
      Categories = new List<string>();
    }
public void SetDealOptions(
      ICollection<DealOption> options)
    {
      Options = options;
    }

    public void AddDealOption(
      DealOption option)
    {
      Options.Add(option);
    }

  }
}
