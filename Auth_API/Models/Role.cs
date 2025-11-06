namespace Auth.Api.Models
{
    public class Role
    {
        public int Id { get; private set; }
        public string Title { get; private set; }

        //Many to many with permissions
        public List<Permission> Permissions { get; private set; }
        public List<RolePermission> RolePermissions { get; private set; }


        //Many to many with users
        public List<User> Users { get; private set; }
        public List<UserRole> UserRole { get; private set; }
        public Role(string title)
        {
            Title = title;
        }
        private Role()
        {

        }
    }
}
