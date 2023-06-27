namespace ScheduledMeets.Persistence.Model;

internal sealed class User
{
    public long Id { get; set; }
    public string Username { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Nickname { get; set; }
    public string? Email { get; set; }

    public IEnumerable<Member>? Attendees { get; set; }
}
