using ScheduledMeets.Internals;

namespace ScheduledMeets.Core.TimeScheduling;

public class SchedulePattern : IEntity
{
    private List<Interval<TimeSpan>>? _intervals;
    private TimeOccurence? _occurence;

    public long Id { get; }

    public List<Interval<TimeSpan>> Intervals
    {
        get => _intervals ??= new();
        set => _intervals = value ?? throw new ArgumentNullException(nameof(value));
    }

    public TimeOccurence Occurence
    {
        get => _occurence ??= new();
        set => _occurence = value ?? throw new ArgumentNullException(nameof(value));
    }

    public IEnumerable<Interval<DateTime>> GetWindow(Interval<DateTime> range)
    {
        throw new NotImplementedException();
    }
}
