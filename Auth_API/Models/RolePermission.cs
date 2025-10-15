namespace Auth_API.Models
{
    public class RolePermission
    {
        public long Id { get; private set; }

        public int PermissionId { get; private set; }
        public Permission Permission { get; private set; }

        public int RoleId { get; private set; }
        public Role Role { get; private set; }

        public RolePermission(int permissionId, int roleId)
        {
            PermissionId = permissionId;
            RoleId = roleId;
        }
        private RolePermission()
        {

        }
    }
}
