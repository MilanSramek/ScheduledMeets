using ScheduledMeets.Internals;

namespace ScheduledMeets.View;

public class MeetView
{
    public long Id { get; set; }
    public MeetsView Meets { get; set; } = null!;

    //public Interval<DateTime> Period { get; set; }

    public IEnumerable<AttendeeView> AcceptingAttendees { get; set; } = null!;
    public IEnumerable<AttendeeView> DecliningAttendees { get; set; } = null!;
}
