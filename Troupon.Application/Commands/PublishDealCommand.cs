using System;
using MediatR;

namespace Troupon.Catalog.Core.Application.Commands
{
    public class PublishDealCommand : IRequest
    {
        public Guid DealId { get; set; }
    }
}
