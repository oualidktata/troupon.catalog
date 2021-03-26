using MediatR;
using Microsoft.Extensions.Logging;
using Troupon.Catalog.Infra.Persistence.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Troupon.Catalog.Core.Application.Events
{
    public class DealCreatedEvent : INotification
    {
        public class DealCreatedEventHandler : INotificationHandler<DealCreatedEvent>
        {

            private readonly IDealRepo _DealRepo;
            private readonly ILogger<DealCreatedEventHandler> _logger;

            public DealCreatedEventHandler(IDealRepo repo, ILogger<DealCreatedEventHandler> logger)//Add DI
            {
                Name = "DealCreatedEventHandler";
                _DealRepo = repo;
                _logger = logger;
            }

            public string Name { get; set; }

            public Task Handle(DealCreatedEvent notification, CancellationToken cancellationToken)
            {
                _logger.LogInformation($"{notification.GetType()} handled by {Name}");
                var allItems = _DealRepo.GetDeals();
                // throw new SomeBusinessException("DealCreatedEvent");
                //var duplicateItem = allItems.FirstOrDefault(i => i.Name == notification.NewName && i.Id != notification.Id);

                //if (duplicateItem != null)
                //{
                //    throw new DealAlreadyExistsException($"Deal already exists for: {duplicateItem.Id}");
                //}

                return Task.FromResult(allItems);
            }
        }

    }
    

}
