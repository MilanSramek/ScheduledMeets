namespace ScheduledMeets.Persistance.Model;

internal sealed class Attendee
{
    public long Id { get; set; }
    internal string? Nickname { get; set; }

    public long UserId { get; set; }
    public User? User { get; set; }

    public long MeetsId { get; set; }
    public Meets? Meets { get; set; }

    public bool IsOwner { get; set; }
}