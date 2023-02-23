using ScheduledMeets.Internals.Collections;

namespace ScheduledMeets.Core.MeetsScheduling;

public class TimeOccurence : IEntity, ISequence<DateTime, DateTime>
{
    public long Id { get; }

    public IEnumerator<DateTime> GetEnumeratorFrom(DateTime cursor)
    {
        throw new NotImplementedException();
    }
}
