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
      private readonly IDapper _dapper;
      private readonly IMapper _mapper;

      public GetDealsQueryHandler(
        IMapper mapper,
        IDapper dapper)
      {
        _mapper = mapper;
        _dapper = dapper;
      }

      public async Task<IEnumerable<DealDto>> Handle(
        GetDealsQuery request,
        CancellationToken cancellationToken)
      {
        //Business logic goes here
        var deals = await Task.FromResult(
          _dapper.GetAll<Deal>(
            $"Select * from [Troupon.Catalog].[Deals]",
            null,
            commandType: CommandType.Text));
        var dealDtos =
          _mapper.Map<IEnumerable<Deal>, IEnumerable<DealDto>>(deals);

        return await Task.FromResult(dealDtos);
      }
    }
  }
}
