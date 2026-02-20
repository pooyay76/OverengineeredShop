using Inventory.Domain.Models;

namespace Inventory.Domain.Contracts
{
    public interface IProductItemInventoryRepository
    {
        Task<ProductItemInventory> GetByIdAsync(long id);
    }
}
