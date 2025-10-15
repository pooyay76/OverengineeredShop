namespace Auth_API.Models
{
    public class UserRole
    {
        public long Id { get; private set; }

        public Guid UserId { get; private set; }
        public User User { get; private set; }

        public int RoleId { get; private set; }
        public Role Role { get; private set; }

        public UserRole(Guid userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
        private UserRole()
        {

        }
    }
}
