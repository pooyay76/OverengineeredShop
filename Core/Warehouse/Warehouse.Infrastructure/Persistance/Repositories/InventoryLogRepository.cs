using Common.Domain.Language.Catalog.ValueObjects;
using Inventory.Domain.Contracts;
using Inventory.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistance.Repositories
{
    public class InventoryLogRepository : IInventoryLogRepository
    {
        private readonly InventoryContext _context;
        public InventoryLogRepository(InventoryContext context)
        {
            _context = context;
        }

        public async Task AddRangeWarehouseLogAsync(List<InventoryLog> warehouseLogList)
        {
            await _context.InventoryLogs.AddRangeAsync(warehouseLogList);
        }

        public async Task AddWarehouseLogAsync(InventoryLog warehouseLog)
        {
            await _context.InventoryLogs.AddAsync(warehouseLog);
        }

        public async Task<List<InventoryLog>> GetItemInventoryLogs(ProductItemId itemId)
        {
            return await _context.InventoryLogs.Where(x=>x.ProductItemId == itemId).ToListAsync();
        }
    }
}
