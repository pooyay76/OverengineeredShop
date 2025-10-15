namespace Auth_API.Models
{
    public class UserRefreshToken
    {
        public long Id { get; private set; }
        public Guid UserId { get; private set; }
        public string RefreshToken { get; private set; }

        public DateTime CreationDateTime { get; } = DateTime.UtcNow;
        public DateTime ExpirationDateTime { get; private set; }

        public User User { get; private set; }
        public bool IsValid { get; private set; } = true;
        public UserRefreshToken(Guid userId, string refreshToken, int expirationMinutes)
        {
            UserId = userId;
            RefreshToken = refreshToken;
            ExpirationDateTime = DateTime.UtcNow + TimeSpan.FromMinutes(expirationMinutes);
            IsValid = true;
        }
        private UserRefreshToken()
        {

        }
    }
}
