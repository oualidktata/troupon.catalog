using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Infra.Common.Models.Exceptions;
using Infra.Persistence.Repositories;
using MediatR;
using Troupon.Catalog.Core.Application.Commands;
using Troupon.Catalog.Core.Application.Events;
using Troupon.Catalog.Core.Domain.Dtos;
using Troupon.Catalog.Core.Domain.Entities.Common;
using Troupon.Catalog.Core.Domain.Entities.Deal;
using Troupon.Catalog.Core.Domain.Entities.Order;

namespace Troupon.Catalog.Core.Application.Handlers.Commands
{
    public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, OrderDto>
    {
        private readonly IWriteRepository<Order> _orderWriteRepo;
        private readonly IReadRepository<Deal> _dealReadRepo;
        private readonly IMapper _mapper;

        public PlaceOrderCommandHandler(
            IWriteRepository<Order> orderWriteRepo,
            IReadRepository<Deal> dealReadRepo,
            IMapper mapper)
        {
            _orderWriteRepo = orderWriteRepo;
            _dealReadRepo = dealReadRepo;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(
            PlaceOrderCommand request,
            CancellationToken cancellationToken)
        {
            var deal = _dealReadRepo.SingleOrDefault(x => x.Id == request.DealId);

            if (deal == null)
            {
                throw new ApplicationException("Deal not found");
            }

            var newOrder = new Order(request.DealId);
            newOrder.SetShippingAddress(
                new Address(
                    request.StreetNumber,
                    request.StreetLine1,
                    request.StreetLine2,
                    request.City,
                    request.Country,
                    request.PostalCode,
                    request.StateProvince));


            var option = deal.GetOption(request.DealOptionId);

            if (option == null)
            {
                throw new ApplicationException("Deal option not found");
            }

            var price = deal.GetOptionPrice(
                request.DealOptionId,
                new Currency("USD"));

            newOrder.AddOrderItem(
                option.Name,
                request.DealOptionId,
                new Price(
                    price.CurrentPrice.Amount,
                    new Currency(price.CurrentPrice.Currency.CurrencyName)),
                1);

            _orderWriteRepo.Create(newOrder);

            await DomainEvents.Raise(new OrderPlacedEvent(newOrder.Id));
            
            var orderDto = _mapper.Map<Order, OrderDto>(newOrder);
            
            return await Task.FromResult(orderDto);
        }
    }
}
