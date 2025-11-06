using Warehouse.Domain.Common.ValueObjects;


public record BillPaidEvent(long BillId, List<ProductItemQuantity> purchasedItems)
{

}

