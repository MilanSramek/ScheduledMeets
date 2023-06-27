namespace ScheduledMeets.Persistence.Model;

internal sealed class Attendance
{
    public long Id { get; set; }

    public long MemberId { get; set; }
    public Member? Member { get; set; }

    public long MeetId { get; set; }
    public Meet? Meet { get; set; }

    public AcceptanceStatus Status { get; set; }
}
