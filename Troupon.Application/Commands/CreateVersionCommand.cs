using AutoMapper;
using MediatR;
using Troupon.Catalog.Core.Domain.Dtos;
using Troupon.Catalog.Core.Domain.Entities;
using Troupon.Catalog.Infra.Persistence.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Troupon.Catalog.Core.Application.Commands
{
    public class CreateDealCommand : IRequest<DealDto>
    {
        public string Major { get; set; }
        public string Minor { get; set; }
        public string Patch { get; set; }
        public class CreateDealCommandHandler : IRequestHandler<CreateDealCommand, DealDto>
        {
            private readonly IDealRepo _repo;

            private readonly IMapper _mapper;

            public CreateDealCommandHandler(IDealRepo repo, IMapper mapper)
            {
                _repo = repo;
                _mapper = mapper;
            }
            public async Task<DealDto> Handle(CreateDealCommand request, CancellationToken cancellationToken)
            {
                var DealToAdd = _mapper.Map<CreateDealCommand, DealEntity>(request);
                var addedDeal = _repo.AddDeal(DealToAdd);
                var DealDto = _mapper.Map<DealEntity, DealDto>(addedDeal);
                return await Task.FromResult(DealDto);
            }
        }
    }
}
