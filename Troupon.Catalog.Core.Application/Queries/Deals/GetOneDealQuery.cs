using AutoMapper;
using MediatR;
using Troupon.Catalog.Core.Domain.Dtos;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infra.Persistence.Dapper;
using Infra.Persistence.Repositories;
using Troupon.Catalog.Core.Domain.Entities.Deal;

namespace Troupon.Catalog.Core.Application.Queries.Deals
{
  public class GetOneDealQuery : IRequest<DealDto>
  {
    public Guid Id { get; set; }

    public class GetOneDealQueryHandler : IRequestHandler<GetOneDealQuery, DealDto>
    {
      private readonly IDapper _dapper;
      private readonly IMapper _mapper;

      public GetOneDealQueryHandler(
        IMapper mapper,
        IDapper dapper)
      {
        _mapper = mapper;
        _dapper = dapper;
      }

      public async Task<DealDto> Handle(
        GetOneDealQuery request,
        CancellationToken cancellationToken)
      {
        //Business logic goes here
        var deal = await Task.FromResult(
          _dapper.Get<Deal>(
            $"Select * from [Troupon.Catalog].[Deals] where Id = {request.Id}",
            null,
            commandType: CommandType.Text));

        var dealDto = _mapper.Map<Deal, DealDto>(deal);

        return await Task.FromResult(dealDto);
      }
    }
  }
}
