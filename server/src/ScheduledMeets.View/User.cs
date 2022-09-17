namespace ScheduledMeets.View;

public class User
{
    public long Id { get; set; }
    public string Username { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Nickname { get; set; }
    public string? Email { get; set; }
}
