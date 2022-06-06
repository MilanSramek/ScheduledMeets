namespace ScheduledMeets.Internals.Extensions;

public static class EnumerableExtensions
{
    public static IReadOnlyList<T> Evaluate<T>(this IEnumerable<T> collection!!) => collection switch
    {
        IReadOnlyList<T> list => list,
        IEnumerable<T> other => other.ToList()
    };

    public static (T Min, T Max) MinMax<T>(this IEnumerable<T> collection!!)
    {
        IEnumerator<T> enumerator = collection.GetEnumerator();
        if (!enumerator.MoveNext()) throw new InvalidOperationException("Collection is empty.");

        T min = enumerator.Current;
        T max = enumerator.Current;

        IComparer<T> comparer = Comparer<T>.Default;
        while (enumerator.MoveNext())
        {
            if (comparer.Compare(max, enumerator.Current) < 0)
                max = enumerator.Current;
            else if (comparer.Compare(min, enumerator.Current) > 0)
                min = enumerator.Current;
        }

        return (min, max);
    }
}
