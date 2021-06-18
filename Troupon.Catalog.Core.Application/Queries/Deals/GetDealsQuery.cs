using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Infra.MediatR.Caching;
using Infra.Persistence.Repositories;
using MediatR;
using Troupon.Catalog.Core.Application.Utility;
using Troupon.Catalog.Core.Domain.Dtos;
using Troupon.Catalog.Core.Domain.Entities.Deal;
using Troupon.Catalog.Core.Domain.InputModels;

namespace Troupon.Catalog.Core.Application.Queries.Deals
{
  public class GetDealsQuery : IRequest<IEnumerable<DealDto>>,
    ICachable
  {
    public string CacheKey { get; }

    public GetDealsQuery(
      SearchDealsFilter filter)
    {
      CacheKey = $"GetDeal-{UtilityMethods.ToHash(filter)}";
    }

    public class GetDealsQueryHandler : IRequestHandler<GetDealsQuery, IEnumerable<DealDto>>
    {
      private readonly IReadRepository<DealView> _dealReadRepo;

      private readonly IMapper _mapper;

      public GetDealsQueryHandler(
        IReadRepository<DealView> dealReadRepo,
        IMapper mapper)
      {
        _dealReadRepo = dealReadRepo;
        _mapper = mapper;
      }

      public async Task<IEnumerable<DealDto>> Handle(
        GetDealsQuery request,
        CancellationToken cancellationToken)
      {
        //Business logic goes here
        var deals = _dealReadRepo.ToList();
        var dealDtos =
          _mapper.Map<IEnumerable<DealView>, IEnumerable<DealDto>>(deals);

        return await Task.FromResult(dealDtos);
      }
    }
  }
}
