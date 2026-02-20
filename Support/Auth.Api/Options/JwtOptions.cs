namespace Auth.Api.Options
{
    public record JwtOptions
    {
        public int TokenTimeout { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
    }
}
