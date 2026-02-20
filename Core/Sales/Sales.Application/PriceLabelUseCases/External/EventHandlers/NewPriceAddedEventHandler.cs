using Common.Application.Contracts;
using Common.Domain.Language.Catalog.Events.Global;
using MediatR;
using Sales.Domain.PriceLabelAgg.Contracts;
using Sales.Domain.PriceLabelAgg.Models;

namespace Sales.Application.PriceLabelUseCases.External.EventHandlers
{
    public class NewPriceAddedEventHandler : INotificationHandler<NewPriceAddedEvent>
    {
        private readonly IPriceHistoryRepository _priceHistoryRepository;
        private readonly IFrameworkUnitOfWork _unitOfWork;
        public NewPriceAddedEventHandler(IPriceHistoryRepository priceHistoryRepository, IFrameworkUnitOfWork unitOfWork)
        {
            _priceHistoryRepository = priceHistoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(NewPriceAddedEvent notification, CancellationToken cancellationToken)
        {
            PriceLabel price = new(notification.ProductItemId,notification.Price);
            await _priceHistoryRepository.AddAsync(price);
            await _unitOfWork.CommitAsync();
        }
    }
}
