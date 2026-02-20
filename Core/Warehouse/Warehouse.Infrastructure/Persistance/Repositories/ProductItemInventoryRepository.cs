using Inventory.Domain.Contracts;
using Inventory.Domain.Models;

namespace Inventory.Infrastructure.Persistance.Repositories
{
    public class ProductItemInventoryRepository : IProductItemInventoryRepository
    {
        private readonly InventoryContext _context;

        public ProductItemInventoryRepository(InventoryContext context)
        {
            _context = context;
        }

        public async Task<ProductItemInventory> GetByIdAsync(long id)
        {
            return await _context.ProductItemInventories.FindAsync(id);
        }
    }
}
