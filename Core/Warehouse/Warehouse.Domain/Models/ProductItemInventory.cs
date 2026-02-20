using Common.Domain.Base;
using Common.Domain.Language.Catalog.ValueObjects;

namespace Inventory.Domain.Models
{
    public class ProductItemInventory : AggregateRootBase<ProductItemId>
    {
        //Primary Key = product item's key



        public int Quantity { get;private set; }



        public List<InventoryLog> WarehouseLogs { get; private set; }

        internal ProductItemInventory(ProductItemId productItemId)
        {
            Quantity = 0;
            Id = productItemId;
        }
        internal void Increase(int quantity)
        {
            Quantity += quantity;
        }
        internal void Decrease(int quantity)
        {
            Quantity -= quantity;
        }
    }
}
