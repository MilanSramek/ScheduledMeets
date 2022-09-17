namespace ScheduledMeets.View;

public class Attendee
{
    public long Id { get; set; }
    public string? Nickname { get; set; }

    public User User { get; set; } = null!;
    public Meet Meets { get; set; } = null!;
    public bool IsOwner { get; set; }
}
