namespace ScheduledMeets.View;

public class AttendanceView : IWithId
{
    public long Id { get; set; }

    public long MeetId { get; set; }
    public long MemberId { get; set; }

    public AcceptanceStatusView Status { get; set; }
}
