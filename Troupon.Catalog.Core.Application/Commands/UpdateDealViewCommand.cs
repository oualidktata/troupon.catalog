using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Infra.Persistence.Repositories;
using MediatR;
using Troupon.Catalog.Core.Domain.Dtos;
using Troupon.Catalog.Core.Domain.Entities.Common;
using Troupon.Catalog.Core.Domain.Entities.Deal;

namespace Troupon.DealManagement.Core.Application.Commands
{
  //TODO: (But more a comment)In the Catalog the DetailView is our Entity, it's a flat representation of a deal(optimized for viewing). Soucrce could be Elastik Search 
  public class UpdateDealViewCommand : IRequest<DealDto>
  {
    public string Description { get; set; }
    public string Title { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int Limitation { get; set; }
    public string OtherConditions { get; set; }

    public class UpdateDealViewCommandHandler : IRequestHandler<UpdateDealViewCommand, DealDto>
    {
      private readonly IWriteRepository<DealView> _dealWriteRepo;
      private readonly IMapper _mapper;

      public UpdateDealViewCommandHandler(
        IWriteRepository<DealView> dealWriteRepo,
        IMapper mapper)
      {
        _dealWriteRepo = dealWriteRepo;
        _mapper = mapper;
      }

      public async Task<DealDto> Handle(
        UpdateDealViewCommand request,
        CancellationToken cancellationToken)
      {
        var dealToAdd = _mapper.Map<UpdateDealViewCommand, DealView>(request);
        var dealOption = new DealOption("Default Option");
        dealOption.SetPrice(new DealPrice(new Currency("USD"),new Price(150,new Currency("USD")),new Price(100,new Currency("USD"))));
        dealToAdd.AddDealOption(dealOption);
        var addedDealView = _dealWriteRepo.Create(dealToAdd);
        var dealDto = _mapper.Map<DealView, DealDto>(addedDealView);

        return await Task.FromResult(dealDto);
      }
    }
  }
}
