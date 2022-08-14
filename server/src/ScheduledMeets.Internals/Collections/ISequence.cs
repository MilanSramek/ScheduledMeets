namespace ScheduledMeets.Internals.Collections;

public interface ISequence<in TCursor, out TElement>
{
    IEnumerator<TElement> GetEnumeratorFrom(TCursor cursor);
}
