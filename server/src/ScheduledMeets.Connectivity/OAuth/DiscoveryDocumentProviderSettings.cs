
using System.ComponentModel.DataAnnotations;

namespace ScheduledMeets.Connectivity.OAuth;

public class DiscoveryDocumentProviderSettings
{
    public const string Section = "OAuth";

    [Required]
    public string Authority { get; set; } = null!;
    public string? DiscoveryDocumentPath { get; set; }
    public List<string>? AdditionalEndpointBaseAddresses { get; set; }
    public TimeSpan? CacheDuration { get; set; }
}
