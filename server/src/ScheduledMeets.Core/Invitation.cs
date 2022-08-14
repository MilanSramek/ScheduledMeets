namespace ScheduledMeets.Core;

public class Invitation : IEntity
{
    public Invitation(long meetsId, string email)
    {
        Email = email ?? throw new ArgumentNullException(nameof(email));
        MeetsId = meetsId;
    }

    public long Id { get; }

    public long MeetsId { get; }
    public string Email { get; }

    public PersonName? PersonName { get; set; }
    public string? Nickname { get; set; }

    public long? UserId { get; set; }
}
