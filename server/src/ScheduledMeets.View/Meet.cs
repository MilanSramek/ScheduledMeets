using ScheduledMeets.Internals;

namespace ScheduledMeets.View;

public class Meet
{
    public long Id { get; set; }
    public Meets Meets { get; set; } = null!;

    public Interval<DateTime> Period { get; set; }

    public IEnumerable<Attendee> AcceptingAttendees { get; set; } = null!;
    public IEnumerable<Attendee> DecliningAttendees { get; set; } = null!;
}
