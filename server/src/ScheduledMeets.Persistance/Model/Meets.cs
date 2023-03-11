namespace ScheduledMeets.Persistance.Model;

internal sealed class Meets
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;

    public IEnumerable<Attendee>? Attendees { get; set; }
}