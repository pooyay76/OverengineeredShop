using Warehouse.Domain.Models;

namespace Warehouse.Domain.Contracts
{
    public interface IWarehouseLogRepository
    {
        Task AddWarehouseLogAsync(WarehouseLog warehouseLog);
        Task AddRangeWarehouseLogAsync(List<WarehouseLog> warehouseLog);
        Task<List<WarehouseLog>> GetInventoryLogs(InventoryId inventoryId);
    }
}
