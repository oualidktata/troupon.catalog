using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Infra.MediatR.Caching;
using Infra.Persistence.Dapper;
using Infra.Persistence.Repositories;
using MediatR;
using Troupon.Catalog.Core.Application.Utility;
using Troupon.Catalog.Core.Domain.Dtos;
using Troupon.Catalog.Core.Domain.Entities.Deal;
using Troupon.Catalog.Core.Domain.InputModels;

namespace Troupon.Catalog.Core.Application.Queries.Deals
{
  public class GetDealsQuery : IRequest<IEnumerable<DealDetailsDto>>, ICachable
  {
    public string CacheKey { get; }

    public GetDealsQuery(DealsSearchFilter filter)
    {
      CacheKey = $"GetDeal-{UtilityMethods.ToHash(filter)}";
    }

    public class GetDealsQueryHandler : IRequestHandler<GetDealsQuery, IEnumerable<DealDetailsDto>>
    {
      private readonly IDapper _dapper;
      private readonly IMapper _mapper;

      public GetDealsQueryHandler(IMapper mapper, IDapper dapper)
      {
        _mapper = mapper;
        _dapper = dapper;
      }

      public async Task<IEnumerable<DealDetailsDto>> Handle(GetDealsQuery request, CancellationToken cancellationToken)
      {
        //Business logic goes here

        var dealsQuery = _dapper.GetAll<DealView>(
            $"Select * from [Troupon.Catalog].[Deals]",
            null,
            commandType: CommandType.Text);

        var deals = await Task.FromResult(dealsQuery);

        var dealDtos = _mapper.Map<IEnumerable<DealView>, IEnumerable<DealDetailsDto>>(deals);
        return await Task.FromResult(dealDtos);
      }
    }
  }
}
