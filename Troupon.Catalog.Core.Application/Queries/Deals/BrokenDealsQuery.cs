using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Infra.MediatR.Caching;
using Infra.Persistence.Dapper;
using MediatR;
using Troupon.Catalog.Core.Application.Utility;
using Troupon.Catalog.Core.Domain.Dtos;
using Troupon.Catalog.Core.Domain.Exceptions;
using Troupon.Catalog.Core.Domain.InputModels;

namespace Troupon.Catalog.Core.Application.Queries.Deals
{
  public class BrokenDealsQuery : IRequest<IEnumerable<DealDto>>, ICachable
  {
    public string CacheKey { get; }

    public BrokenDealsQuery(SearchDealsFilter filter)
    {
      CacheKey = $"GetDeal-{UtilityMethods.ToHash(filter)}";
    }

    public class BrokenDealsQueryHandler : IRequestHandler<BrokenDealsQuery, IEnumerable<DealDto>>
    {
      public BrokenDealsQueryHandler()
      {
      }

      public async Task<IEnumerable<DealDto>> Handle(BrokenDealsQuery request, CancellationToken cancellationToken)
      {
        throw new DealDoesntExistException("These are not the deals you are looking for!");
      }
    }
  }
}
