namespace ScheduledMeets.View;

public sealed class UserView : IWithId
{
    public long Id { get; set; }
    public string Username { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Nickname { get; set; }
    public string? Email { get; set; }
}
