using System;
using MediatR;

namespace Troupon.Catalog.Core.Application.Commands
{
    public class ProcessPaymentCommand : IRequest<Unit>
    {
        public Guid OrderId { get; set; }

        public ProcessPaymentCommand(
            Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
