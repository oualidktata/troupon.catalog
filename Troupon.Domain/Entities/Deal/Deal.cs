using System;
using System.Collections.Generic;
using Infra.DomainDrivenDesign.Base;
using Troupon.Catalog.Core.Domain.Entities.Account;
using Troupon.Catalog.Core.Domain.Entities.Category;
using Troupon.Catalog.Core.Domain.Enums;

namespace Troupon.Catalog.Core.Domain.Entities.Deal
{
    public class DealId : EntityId
    {
        
    }
    
    public class Deal : AggregateRoot<DealId>
    {
        public string Description { get; private set; }
        public string Title { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public int Limitation { get; private set; }
        public string OtherConditions { get; private set; }
        public DealStatus Status { get; private set; }
        public AccountId AccountId { get; private set; }
        public ICollection<DealOption> Options { get; private set; }
        public ICollection<DealCategoryId> CategoryIds { get; private set; }

        public Deal()
        {
            Options = new List<DealOption>();
            CategoryIds = new List<DealCategoryId>();
        }

        public void Publish()
        {
            // Do Some Stuff and Validation
            if (!CanPublish()) return;
            Status = DealStatus.Published;
        }

        public bool CanPublish()
        {
            // Do other validations
            return Status == DealStatus.Draft;
        }

        public void End()
        {
            Status = DealStatus.Ended;
        }

        public bool CanEnd()
        {
            return Status == DealStatus.Published;
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

        public void SetCategories(
            ICollection<DealCategoryId> categoryIds)
        {
            CategoryIds = categoryIds;
        }

        public void AddCategory(
            DealCategoryId categoryId)
        {
            CategoryIds.Add(categoryId);
        }
    }
}
