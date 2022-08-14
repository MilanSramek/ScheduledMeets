namespace ScheduledMeets.Core;

public class Attendee : IEntity
{
    internal Attendee(long userId, long meetsId)
    {
        UserId = userId;
        MeetsId = meetsId;
    }

    public long Id { get; }
    public string? Nickname { get; internal set; }

    public long UserId { get; }
    public long MeetsId { get; }
    public bool IsOwner { get; internal set; }
}
