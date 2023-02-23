namespace ScheduledMeets.View;

public class UserView : IWithId
{
    public long Id { get; set; }
    public string Username { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Nickname { get; set; }
    public string? Email { get; set; }
    public IEnumerable<AttendeeView> Attendees { get; set; } = null!;
}
