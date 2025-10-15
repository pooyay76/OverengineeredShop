namespace Auth_API.Models
{
    public class Permission
    {
        public int Id { get; private set; }
        public int ApplicationId { get; private set; }
        public string Title { get; private set; }
        public string Value { get; private set; }

        //Many to many with Role
        public List<RolePermission> RolePermissions { get; private set; }
        public List<Role> Roles { get; private set; }

        public Permission(int applicationId, string title, string value)
        {
            ApplicationId = applicationId;
            Title = title;
            Value = value;
        }
        private Permission()
        {

        }
    }
}
