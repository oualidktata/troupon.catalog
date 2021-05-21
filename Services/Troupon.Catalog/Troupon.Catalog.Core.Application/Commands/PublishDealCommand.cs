using System;
using MediatR;

namespace Troupon.Catalog.Core.Application.Commands
{
  public class PublishDealCommand : IRequest<Unit>
  {
    public Guid DealId { get; set; }
  }
}
