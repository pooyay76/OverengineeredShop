using Sales.Domain.BillAgg.Contracts;
using Sales.Domain.BillAgg.Models;
using Sales.Domain.Common;
using Sales.Domain.Common.ValueObjects;
using Sales.Domain.OrderAgg.Exceptions;
using Sales.Domain.OrderAgg.Models;

namespace Sales.Domain.OrderAgg
{
    public class OrderManager
    {
        private readonly IBillRepository billRepository;

        public OrderManager(IBillRepository billRepository)
        {
            this.billRepository = billRepository;
        }

        public async Task<Order> CreateOrderAsync(BillId billId, CustomerId customerId)
        {
            Bill bill = await billRepository.GetActiveAsync(x => x.Id == billId && x.CustomerId == customerId);
            if (bill == null)
                throw new BillNotPaidException();
            List<ProductItemQuantity> boughtProducts = bill.BillItems.Select(x => new ProductItemQuantity()
            {
                ProductItemId = x.ProductItemId,
                Quantity = x.Quantity
            }).ToList();
            Order order = new(customerId, billId, bill.TotalBilling, boughtProducts, true);
            return order;
        }
    }
}
