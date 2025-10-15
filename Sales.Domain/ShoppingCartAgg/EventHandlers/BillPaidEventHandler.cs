using Sales.Domain._common.Base;
using Sales.Domain.BillAgg.Contracts;
using Sales.Domain.BillAgg.Events;
using Sales.Domain.ShoppingCartAgg.Contracts;

namespace Sales.Domain.ShoppingCartAgg.EventHandlers
{
    public class BillPaidEventHandler : IDomainEventHandler<BillPaidEvent>
    {
        private readonly IBillRepository billRepository;
        private readonly IShoppingCartRepository shoppingCartRepository;

        public BillPaidEventHandler(IBillRepository billRepository, IShoppingCartRepository shoppingCartRepository)
        {
            this.billRepository = billRepository;
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public async Task Handle(BillPaidEvent notification, CancellationToken cancellationToken)
        {

            var bill = await billRepository.GetAsync(x => x.Id == notification.AggregateId);
            var cart = await shoppingCartRepository.GetAsync(x => x.Id == notification.CustomerId);

            if (cart != null & bill != null)
            {
                cart.ClearItems(bill.BillItems.Select(x => x.ProductItemId).ToList());
            }
            await shoppingCartRepository.UpdateAsync(cart);
        }
    }
}
