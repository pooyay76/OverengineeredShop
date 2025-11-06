using Warehouse.Domain.Common.Base;

namespace Warehouse.Domain.Models
{
    public class Inventory : AggregateRoot<long>
    {
        //Primary Key = item's key
        public long ProductItemId { get; private set; }
        public int Quantity { get;private set; }

        internal Inventory(long productItemId)
        {
            Quantity = 0;
            ProductItemId = productItemId;
        }
        internal void Increase(int quantity)
        {
            Quantity += quantity;
        }
        internal void Decrease(int quantity)
        {
            Quantity -= quantity;
        }
        public List<WarehouseLog> InventoryLogs { get; private set; }
    }
}
