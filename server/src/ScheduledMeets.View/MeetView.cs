namespace ScheduledMeets.View;

public class MeetView : IWithId
{
    public long Id { get; set; }

    public long MeetsId { get; set; }

    public DateTimeOffset From { get; set; }
    public DateTimeOffset To { get; set; }

    public IEnumerable<MemberView>? AcceptingAttendees { get; set; }
    public IEnumerable<MemberView>? DecliningAttendees { get; set; }
    public IEnumerable<MemberView>? IdleAttendees { get; set; }
}
