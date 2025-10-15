namespace Auth_API.Contracts
{
    public interface IPermissionService
    {
        public bool CheckPermission(Guid userId, string permission);
    }
}
