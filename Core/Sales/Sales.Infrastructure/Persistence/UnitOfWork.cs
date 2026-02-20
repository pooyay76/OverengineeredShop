using Common.Application.Contracts;

namespace Sales.Infrastructure.Persistence
{
    public class UnitOfWork : IFrameworkUnitOfWork
    {
        private readonly SalesDbContext salesDbContext;

        public UnitOfWork(SalesDbContext salesDbContext, IMediator mediator)
        {
            this.salesDbContext = salesDbContext;
        }

        public async Task CommitAsync()
        {

            await salesDbContext.SaveChangesAsync();
        }

    }
}
