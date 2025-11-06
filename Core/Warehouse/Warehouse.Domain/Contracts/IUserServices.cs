using Warehouse.Domain.Common.ValueObjects;

namespace Warehouse.Domain.Contracts
{
    public interface IUserServices
    {
        string GetCurrentUserFullName();
        UserId GetCurrentUserId();
    }
}
