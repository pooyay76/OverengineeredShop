using Common.Domain.Language.Global.ValueObjects;

namespace Inventory.Domain.Contracts
{
    public interface IUserServices
    {
        string GetCurrentUserFullName();
        UserId GetCurrentUserId();
    }
}
