using MediatR;
using Sales.Domain.BillAgg.Events;
using Sales.Domain.OrderAgg.Contracts;
using Sales.Domain.OrderAgg.Models;

namespace Sales.Domain.OrderAgg.EventHandlers
{
    public class CreateOrderOnBillPaidEventHandler : INotificationHandler<BillPaidEvent>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IWarehouseServices warehouseServices;
        public CreateOrderOnBillPaidEventHandler(IOrderRepository orderRepository, IWarehouseServices warehouseServices)
        {
            this.orderRepository = orderRepository;
            this.warehouseServices = warehouseServices;
        }

        public async Task Handle(BillPaidEvent @event, CancellationToken cancellationToken)
        {
            bool reserveResult = await warehouseServices.ReserveItems(@event.PurchasedItems);
            Order order = new(@event.CustomerId, @event.AggregateId, @event.Amount, @event.PurchasedItems, reserveResult);
            await orderRepository.CreateAsync(order);
        }

    }
}
