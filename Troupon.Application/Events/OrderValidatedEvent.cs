using System;
using Infra.DomainDrivenDesign.Base;
using MediatR;

namespace Troupon.Catalog.Core.Application.Events
{
    public class OrderValidatedEvent : INotification, IDomainEvent
    {
        public Guid OrderId { get; set; }
        
        public OrderValidatedEvent(Guid orderId)
        {
            OrderId = orderId;
            CreationDate = DateTime.UtcNow;
        }
        public DateTime CreationDate { get; set; }
    }
}
