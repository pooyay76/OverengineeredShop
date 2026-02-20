using Common.Domain.Language.Catalog.ValueObjects;
using Inventory.Domain.Models;

namespace Inventory.Domain.Contracts
{
    public interface IInventoryLogRepository
    {
        Task AddWarehouseLogAsync(InventoryLog warehouseLog);
        Task AddRangeWarehouseLogAsync(List<InventoryLog> warehouseLog);
        Task<List<InventoryLog>> GetItemInventoryLogs(ProductItemId inventoryId);
    }
}
