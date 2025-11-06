using MediatR;
using Sales.Application.Contracts;

namespace Sales.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SalesDbContext salesDbContext;

        public UnitOfWork(SalesDbContext salesDbContext, IMediator mediator)
        {
            this.salesDbContext = salesDbContext;
        }

        public async Task CommitTransactions()
        {

            await salesDbContext.SaveChangesAsync();
        }

    }
}
