namespace ScheduledMeets.Internals.Extensions;

public static class EnumerableExtensions
{
    public static IReadOnlyList<T> Evaluate<T>(this IEnumerable<T> sequence)
    {
        ArgumentNullException.ThrowIfNull(sequence);

        if (sequence is IReadOnlyList<T> list)
            return list;

        if (sequence is IReadOnlyCollection<T> { Count: > 0 } 
            || sequence is ICollection<T> { Count: > 0 })
        {
            return sequence.ToList();
        }

        IEnumerator<T> enumerator = sequence.GetEnumerator();
        if (!enumerator.MoveNext())
            return Array.Empty<T>();

        List<T> result = new();
        do result.Add(enumerator.Current);
        while (enumerator.MoveNext());

        return result;
    }

    public static (T Min, T Max) MinMax<T>(this IEnumerable<T> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);

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
