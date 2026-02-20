using Common.Application.Contracts;

namespace Common.Infrastructure.Persistence
{
    public class FrameworkUnitOfWork: IFrameworkUnitOfWork
    {
        private readonly FrameworkDbContext _context;
        public FrameworkUnitOfWork(FrameworkDbContext context)
        {
            _context = context;
        }
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
