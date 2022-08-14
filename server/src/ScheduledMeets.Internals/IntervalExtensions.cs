namespace ScheduledMeets.Internals;

public static class IntervalExtensions
{
    public static Interval<T>? Intersection<T>(this Interval<T> interval, Interval<T> other)
        where T : IComparable<T>
    {
        T from = Math.Max(interval.From, other.From);
        T to = Math.Min(interval.To, other.To);

        return from.CompareTo(to) < 0
            ? new(from, to)
            : null;
    }
}
