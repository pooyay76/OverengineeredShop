namespace Common.Infrastructure.Config
{
    public static class ConfigValues
    {
        public static TimeSpan EventConsumerBackgroundDelay { get; private set; } = TimeSpan.FromMilliseconds(200);
    }
}
