using AutoMapper;
using MediatR;
using Troupon.Catalog.Core.Domain.Dtos;
using Troupon.Catalog.Core.Domain.Entities;
using Troupon.Catalog.Infra.Persistence.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Troupon.Catalog.Core.Application.Queries.Deals
{
    public class GetOneDealQuery : IRequest<DealDto>
    {
        public Guid Id { get; set; }

        public class GetOneDealQueryHandler : IRequestHandler<GetOneDealQuery, DealDto>
        {
            private readonly IDealRepo _repo;

            private readonly IMapper _mapper;

            public GetOneDealQueryHandler(IDealRepo repo, IMapper mapper)
            {
                _repo = repo;
                _mapper = mapper;
            }
            public async Task<DealDto> Handle(GetOneDealQuery request, CancellationToken cancellationToken)
            {
                //Business logic goes here
                var Deal = _repo.GetDeal(request.Id);
                var DealDto = _mapper.Map<DealEntity, DealDto>(Deal);
                return await Task.FromResult(DealDto);
            }
        }
    }
}
