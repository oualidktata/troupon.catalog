using AutoMapper;
using MediatR;
using Troupon.Catalog.Core.Domain.Dtos;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infra.Persistence.Repositories;
using Troupon.Catalog.Core.Domain.Entities.Deal;

namespace Troupon.Catalog.Core.Application.Queries.Deals
{
  public class GetOneDealQuery : IRequest<DealDto>
  {
    public Guid Id { get; set; }

    public class GetOneDealQueryHandler : IRequestHandler<GetOneDealQuery, DealDto>
    {
      private readonly IReadRepository<DealView> _dealReadRepo;

      private readonly IMapper _mapper;

      public GetOneDealQueryHandler(
        IReadRepository<DealView> dealReadRepo,
        IMapper mapper)
      {
        _dealReadRepo = dealReadRepo;
        _mapper = mapper;
      }

      public async Task<DealDto> Handle(
        GetOneDealQuery request,
        CancellationToken cancellationToken)
      {
        //Business logic goes here
        var deal = _dealReadRepo.SingleOrDefault(x => x.Id == request.Id);
        var dealDto = _mapper.Map<DealView, DealDto>(deal);

        return await Task.FromResult(dealDto);
      }
    }
  }
}
