namespace ScheduledMeets.View;

public class MeetsView : IWithId
{
    public long Id { get; set; }
    public string? Name { get; set; }

    public IEnumerable<AttendeeView> Owners { get; set; } = null!;
    public IEnumerable<AttendeeView> Attendees { get; set; } = null!;

    public IEnumerable<MeetView> ContainedMeets { get; set; } = null!;
}