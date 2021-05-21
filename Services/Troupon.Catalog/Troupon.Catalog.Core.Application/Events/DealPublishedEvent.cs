using System;
using System.Threading;
using System.Threading.Tasks;
using Infra.DomainDrivenDesign.Base;
using MediatR;

namespace Troupon.Catalog.Core.Application.Events
{
    public class DealPublishedEvent : INotification, IDomainEvent
    {
        public DateTime CreationDate { get; set; }

        public DealPublishedEvent()
        {
            CreationDate = DateTime.UtcNow;
        }
    }
}
