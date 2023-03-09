namespace ScheduledMeets.Core;

public class User : IEntity
{
    public User(string username)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
    }

    public long Id { get; private set; }
    public string Username { get; }
    public PersonName? Name { get; set; }
    public string? Nickname { get; set; }
    public string? Email { get; set; }
}
