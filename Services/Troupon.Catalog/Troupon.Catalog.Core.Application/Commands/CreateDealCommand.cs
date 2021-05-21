using System;
using AutoMapper;
using MediatR;
using Troupon.Catalog.Core.Domain.Dtos;
using System.Threading;
using System.Threading.Tasks;
using Infra.Persistence.Repositories;
using Troupon.Catalog.Core.Domain.Entities.Common;
using Troupon.Catalog.Core.Domain.Entities.Deal;

namespace Troupon.Catalog.Core.Application.Commands
{
  public class CreateDealCommand : IRequest<DealDto>
  {
    public string Description { get; set; }
    public string Title { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int Limitation { get; set; }
    public string OtherConditions { get; set; }

    public class CreateDealCommandHandler : IRequestHandler<CreateDealCommand, DealDto>
    {
      private readonly IWriteRepository<Deal> _dealWriteRepo;
      private readonly IMapper _mapper;

      public CreateDealCommandHandler(
        IWriteRepository<Deal> dealWriteRepo,
        IMapper mapper)
      {
        _dealWriteRepo = dealWriteRepo;
        _mapper = mapper;
      }

      public async Task<DealDto> Handle(
        CreateDealCommand request,
        CancellationToken cancellationToken)
      {
        var dealToAdd = _mapper.Map<CreateDealCommand, Deal>(request);
        var dealOption = new DealOption("Default Option");
        dealOption.SetPrice(
          new DealPrice(
            new Currency("USD"),
            new Price(
              150,
              new Currency("USD")),
            new Price(
              100,
              new Currency("USD"))));
        dealToAdd.AddDealOption(dealOption);
        var addedDeal = _dealWriteRepo.Create(dealToAdd);
        var dealDto = _mapper.Map<Deal, DealDto>(addedDeal);

        return await Task.FromResult(dealDto);
      }
    }
  }
}
