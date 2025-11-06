using Warehouse.Domain.Models;

namespace Warehouse.Domain.Contracts
{
    public interface IInventoryRepository
    {
        Task<Inventory> GetByIdAsync(long id);
    }
}
