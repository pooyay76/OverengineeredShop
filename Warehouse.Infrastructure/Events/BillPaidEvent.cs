using Sales.Domain.Common.ValueObjects;


public record BillPaidEvent(long BillId, List<ProductItemQuantity> purchasedItems)
{

}

