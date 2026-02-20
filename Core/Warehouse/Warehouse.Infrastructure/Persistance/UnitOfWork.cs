namespace Inventory.Infrastructure.Persistance
{
    public class UnitOfWork
    {
        private readonly InventoryContext _context;
        public UnitOfWork(InventoryContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
