namespace Sales.Application.Contracts
{
    public interface IUnitOfWork
    {
        Task CommitTransactions();
    }
}
