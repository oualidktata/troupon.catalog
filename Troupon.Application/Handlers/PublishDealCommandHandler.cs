using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infra.Persistence.Repositories;
using MediatR;
using Troupon.Catalog.Core.Application.Commands;
using Troupon.Catalog.Core.Application.Events;
using Troupon.Catalog.Core.Domain.Entities.Deal;

namespace Troupon.Catalog.Core.Application.Handlers
{
    public class PublishDealCommandHandler : IRequestHandler<PublishDealCommand, Unit>
    {
        private readonly IReadRepository<Deal> _dealReadRepo;
        private readonly IWriteRepository<Deal> _dealWriteRepo;

        public PublishDealCommandHandler(
            IReadRepository<Deal> dealReadRepo,
            IWriteRepository<Deal> dealWriteRepo)
        {
            _dealReadRepo = dealReadRepo;
            _dealWriteRepo = dealWriteRepo;
        }

        public async Task<Unit> Handle(
            PublishDealCommand request,
            CancellationToken cancellationToken)
        {
            var deal = _dealReadRepo.SingleOrDefault(x => x.Id == request.DealId);

            if (deal == null) return await Task.FromResult(Unit.Value);
            
            deal.Publish();
            _dealWriteRepo.Update(deal);
            await DomainEvents.Raise(new DealPublishedEvent());

            return await Task.FromResult(Unit.Value);
        }
    }
}
