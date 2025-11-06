namespace Auth.Api.Contracts
{
    public interface IPermissionService
    {
        public bool CheckPermission(Guid userId, string permission);
    }
}
