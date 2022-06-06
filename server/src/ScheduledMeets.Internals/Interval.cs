namespace ScheduledMeets.Internals;

public struct Interval<T> : IEquatable<Interval<T>> where T : IComparable<T>
{
    public Interval(T from, T to)
    {
        if (from.CompareTo(to) > 0) throw new ArgumentOutOfRangeException(nameof(from));

        From = from;
        To = to;
    }

    public T From { get; }
    public T To { get; }

    public Interval<T>? Intersection(Interval<T> other)
    {
        T from = Math.Max(From, other.From);
        T to = Math.Min(To, other.To);

        return from.CompareTo(to) < 0
            ? new(from, to)
            : null;
    }

    public bool Contains(Interval<T> other) => From.CompareTo(other.From) <= 0 
        && To.CompareTo(other.To) >= 0;

    public bool Equals(Interval<T> other) => From.CompareTo(other.From) == 0
        && To.CompareTo(other.To) == 0;

    public override string ToString() => $"[{From}, {To})";

    public override bool Equals(object? obj) => obj is Interval<T> other && Equals(other);

    public static bool operator ==(Interval<T> left, Interval<T> right) => left.Equals(right);

    public static bool operator !=(Interval<T> left, Interval<T> right) => !(left == right);
}
