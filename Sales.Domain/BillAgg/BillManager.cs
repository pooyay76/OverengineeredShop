
using Sales.Domain._common;
using Sales.Domain._common.ValueObjects;
using Sales.Domain.BillAgg.Contracts;
using Sales.Domain.BillAgg.Exceptions;
using Sales.Domain.BillAgg.Models;
using Sales.Domain.DiscountAgg.Contracts;
using Sales.Domain.DiscountAgg.Models;
using Sales.Domain.PriceHistoryAgg.Contracts;
using Sales.Domain.PriceHistoryAgg.Models;
using Sales.Domain.ShoppingCartAgg.Contracts;

namespace Sales.Domain.BillAgg
{
    public class BillManager
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IDiscountRepository discountRepository;
        private readonly IPriceHistoryRepository priceHistoryRepository;
        private readonly IPaymentGatewayService paymentGatewayService;
        private readonly IBillRepository billRepository;
        private readonly IShippingService shippingService;

        public BillManager(IShoppingCartRepository shoppingCartRepository,
            IDiscountRepository discountRepository, IPriceHistoryRepository priceHistoryRepository,
            IPaymentGatewayService paymentGatewayService, IShippingService shippingService)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.discountRepository = discountRepository;
            this.priceHistoryRepository = priceHistoryRepository;
            this.paymentGatewayService = paymentGatewayService;
            this.shippingService = shippingService;
        }
        public async Task<Bill> CreateAsync(CustomerId customerId, CustomerType userRole, ShippingInformation receiverInformation)
        {
            var cart = await shoppingCartRepository.GetOrThrowAsync(x => x.Id == customerId);

            //validate shopping cart
            if (cart.ShoppingCartItems.Count == 0)
                throw new ShoppingCartEmptyException();

            var activeBill = await billRepository.GetActiveAsync(x => x.CustomerId == customerId);


            if (activeBill != null)
            {
                var cartItems = cart.ShoppingCartItems.Select(x => x.ProductItemId).ToList();
                var existingBillItems = activeBill.BillItems.Select(x => x.ProductItemId).ToList();

                //if the cart hasn't change and bill is active, then the bill is still valid and can be used
                if (cartItems.Count == existingBillItems.Count && !cartItems.Except(existingBillItems).Any())
                {
                    return activeBill;
                }

            }


            //get active discounts
            List<Discount> activeDiscounts = await discountRepository.GetActiveDiscountsAsync();


            DiscountTargetType targetType = userRole == CustomerType.Normal ? DiscountTargetType.Everyone :
                userRole == CustomerType.Admin ? DiscountTargetType.Admin : DiscountTargetType.Colleague;

            List<Discount> userDiscounts = activeDiscounts.Where(x => x.TargetType == targetType ||
            x.TargetType == DiscountTargetType.Everyone).ToList();

            Discount billDiscount = userDiscounts
                .Where(x => x.DiscountType == DiscountType.BillDiscount)
                .MaxBy(x => x.DiscountPercentage);

            List<PriceLabel> prices = await priceHistoryRepository
                .GetLatestPricesAsync(x => cart.ShoppingCartItems.Any(y => y.ProductItemId == x.ProductItemId));

            if (prices.Count != cart.ShoppingCartItems.Count)
                throw new AShoppingCartItemIsUnavailableException();


            List<BillItem> billItems = cart.ShoppingCartItems
                .Join(prices, cartItem => cartItem.ProductItemId, price => price.ProductItemId,
                (cartItem, price) => new BillItem(cartItem.ProductItemId, cartItem.Quantity,
                price.Price,
                userDiscounts.FirstOrDefault(dis => dis.DiscountType == DiscountType.ProductDiscount &&
                dis.ProductItemId == cartItem.ProductItemId))).ToList();



            #region REFACTOR LATER
            //this section needs to change,
            //we want multiple payment sessions per bill


            //later, validate order item availability from warehouse
            Money shipmentValue = new Money(billItems.Sum(x => x.UnitPriceBase.GetValue()));

            var shippingCost = shippingService.GetShippingCost(shipmentValue,
                billItems.Select(x => new ProductItemQuantity()
                {
                    ProductItemId = x.ProductItemId,
                    Quantity = x.Quantity
                }).ToList(),
                receiverInformation);

            Bill bill = new(customerId, billItems, shippingCost, billDiscount);

            string sessionId = await paymentGatewayService.GetNewSessionIdAsync(bill.TotalBilling, bill.Id);
            bill.SetSession(sessionId);
            //create a bill which is valid for X minutes defined in constants namespace
            #endregion



            await billRepository.CreateAsync(bill);

            return bill;
        }

        public async Task<Bill> UpdateStatus(BillId billId)
        {

            var bill = await billRepository.GetOrThrowAsync(x => x.Id == billId);
            if (bill.BillStatus != BillStatus.AwaitingPayment)
                throw new CannotChangeBillStatusAfterItIsSetException();
            if (await paymentGatewayService.VerifyPaymentAsync(bill.PaymentSessionId, bill.TotalBilling))
            {
                bill.MarkAsPaid();
            }
            else
            {
                bill.MarkAsFailed();
            }
            return bill;
        }
    }
}
