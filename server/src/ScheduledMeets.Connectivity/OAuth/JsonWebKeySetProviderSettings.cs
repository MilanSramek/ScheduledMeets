namespace ScheduledMeets.Connectivity.OAuth;

public class JsonWebKeySetProviderSettings
{
    public const string Section = "OAuth";

    public TimeSpan? CacheDuration { get; set; }
}

