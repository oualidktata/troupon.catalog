using System;
using System.Threading;
using System.Threading.Tasks;
using Infra.DomainDrivenDesign.Base;
using MediatR;

namespace Troupon.Catalog.Core.Application.Events
{
    public class DealPublishedEvent : INotification, IDomainEvent
    {
        public class DealPublishedEventHandler : INotificationHandler<DealPublishedEvent>
        {

            public DealPublishedEventHandler()
            {
                
            }

            public Task Handle(
                DealPublishedEvent notification,
                CancellationToken cancellationToken)
            {
                // Trigger another command
                
                return Task.CompletedTask;
            }
        }

        public DateTime CreationDate { get; set; }
    }
}
