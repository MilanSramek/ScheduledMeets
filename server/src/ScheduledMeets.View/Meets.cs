namespace ScheduledMeets.View;

public class Meets
{
    public long Id { get; set; }
    public string? Name { get; set; }

    public IEnumerable<Attendee> Owners { get; set; } = null!;
    public IEnumerable<Attendee> Attendees { get; set; } = null!;

    public IEnumerable<Meet> ContainedMeets { get; set; } = null!;
}