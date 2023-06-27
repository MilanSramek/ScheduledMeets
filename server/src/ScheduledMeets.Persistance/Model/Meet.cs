namespace ScheduledMeets.Persistence.Model;

internal sealed class Meet
{
    public long Id { get; set; }

    public long MeetsId { get; set; }
    public Meets? Meets { get; set; }

    public DateTimeOffset From { get; set; }
    public DateTimeOffset To { get; set; }

    public IEnumerable<Attendance>? Attendances { get; set; }
}
