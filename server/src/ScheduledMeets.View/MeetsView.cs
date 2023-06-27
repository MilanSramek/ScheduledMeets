namespace ScheduledMeets.View;

public class MeetsView : IWithId
{
    public long Id { get; set; }
    public string? Name { get; set; }

    public IEnumerable<MemberView> Attendees { get; set; } = null!;
}