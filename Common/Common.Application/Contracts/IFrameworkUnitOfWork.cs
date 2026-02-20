
namespace Common.Application.Contracts
{
    public interface IFrameworkUnitOfWork
    {
        Task CommitAsync();
    }
}
