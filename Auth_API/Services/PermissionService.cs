using Auth_API.Contracts;
using Auth_API.Data;

namespace Auth_API.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly AuthContext context;

        public PermissionService(AuthContext context)
        {
            this.context = context;
        }

        public bool CheckPermission(Guid userId, string permission)
        {
            var userRoles = context.UserRoles.Where(x => x.UserId == userId);
            var rolePermissions = context.RolePermissions.Where(x => userRoles.Any(y => y.RoleId == x.RoleId));
            var permissions = context.Permissions.Where(x => rolePermissions.Any(y => x.Id == y.PermissionId));
            return permissions.Any(x => x.Value == permission);
        }
    }
}
