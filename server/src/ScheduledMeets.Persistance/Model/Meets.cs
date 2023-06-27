namespace ScheduledMeets.Persistence.Model;

internal sealed class Meets
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;

    public IEnumerable<Meet>? ContainedMeets { get; set; }
    public IEnumerable<Member>? Members { get; set; }
}