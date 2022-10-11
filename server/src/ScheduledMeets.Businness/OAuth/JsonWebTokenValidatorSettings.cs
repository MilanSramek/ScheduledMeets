
using System.ComponentModel.DataAnnotations;

namespace ScheduledMeets.Business.OAuth;

public class JsonWebTokenValidatorSettings
{
    public const string Section = "OAuth";

    [Required]
    public string Audience { get; set; } = null!;
}
