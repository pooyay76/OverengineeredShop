using Common.Domain.Base;
using Common.Domain.Language.Sales.Events.Global;
using Sales.Domain.BillAgg.Contracts;
using Sales.Domain.ShoppingCartAgg.Contracts;

namespace Sales.Domain.ShoppingCartAgg.EventHandlers
{
    public class BillPaidEventHandler : EventHandlerBase<BillPaidEvent>
    {
        private readonly IBillRepository billRepository;
        private readonly IShoppingCartRepository shoppingCartRepository;
        public BillPaidEventHandler(IBillRepository billRepository, IShoppingCartRepository shoppingCartRepository)
        {
            this.billRepository = billRepository;
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public override async Task HandleAsync(BillPaidEvent notification)
        {

            var bill = await billRepository.GetAsync(x => x.Id == notification.AggregateId);
            var cart = await shoppingCartRepository.GetAsync(x => x.Id == notification.CustomerId);

            if (cart != null && bill != null)
            {
                //since bill is paid shopping cart is cleared from bought items
                cart.ClearItems(bill.BillItems.Select(x => x.ProductItemId).ToList());
            }
            await shoppingCartRepository.UpdateAsync(cart);
        }
    }
}
