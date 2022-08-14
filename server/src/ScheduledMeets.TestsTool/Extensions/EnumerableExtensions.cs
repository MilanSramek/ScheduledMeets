namespace ScheduledMeets.TestTools.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<TResult> Select<TElement, TResult>(
        this IEnumerable<TElement> sequence,
        Func<TElement, TElement, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(sequence);
        ArgumentNullException.ThrowIfNull(selector);

        IEnumerator<TElement> enumerator = sequence.GetEnumerator();

        if (!enumerator.MoveNext())
            yield break;
        TElement previous = enumerator.Current;

        if (!enumerator.MoveNext())
            yield break;
        TElement current = enumerator.Current;

        yield return selector(previous, current);
        while (enumerator.MoveNext())
        {
            previous = current;
            current = enumerator.Current;

            yield return selector(previous, current);
        }
    }
}
