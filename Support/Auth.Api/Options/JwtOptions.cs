namespace Auth.Api.Options
{
    public record JwtOptions
    {
        public string TokenKey { get; set; }
        public int TokenTimeout { get; set; }
    }
}
