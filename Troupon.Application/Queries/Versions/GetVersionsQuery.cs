using AutoMapper;
using MediatR;
using Troupon.Catalog.Core.Application.Caching;
using Troupon.Catalog.Core.Application.Utility;
using Troupon.Catalog.Core.Domain.Dtos;
using Troupon.Catalog.Core.Domain.Entities;
using Troupon.Catalog.Core.Domain.InputModels;
using Troupon.Catalog.Infra.Persistence.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Troupon.Catalog.Core.Application.Queries.Deals
{
    public class GetDealsQuery : IRequest<IEnumerable<DealDto>>,ICachable
    {
        public string CacheKey { get; }
        public GetDealsQuery(SearchDealsFilter filter)
        {
            CacheKey=$"GetDeal-{UtilityMethods.ToHash(filter)}";
        }
        public class GetDealsQueryHandler : IRequestHandler<GetDealsQuery, IEnumerable<DealDto>>
        {
            private readonly IDealRepo _repo;

            private readonly IMapper _mapper;

            public GetDealsQueryHandler(IDealRepo repo, IMapper mapper)
            {
                _repo = repo;
                _mapper = mapper;
            }
            public async Task<IEnumerable<DealDto>> Handle(GetDealsQuery request, CancellationToken cancellationToken)
            {
                //Business logic goes here
                var Deals = _repo.GetDeals();
                var DealDtos = _mapper.Map<IEnumerable<DealEntity>, IEnumerable<DealDto>>((IEnumerable<DealEntity>)Deals);
                return await Task.FromResult(DealDtos);
            }
        }
    }
}
