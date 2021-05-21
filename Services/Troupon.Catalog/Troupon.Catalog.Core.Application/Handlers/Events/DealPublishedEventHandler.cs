using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Troupon.Catalog.Core.Application.Events;

namespace Troupon.Catalog.Core.Application.Handlers.Events
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
}
