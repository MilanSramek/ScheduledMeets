using IntervalTree;

using ScheduledMeets.Internals.Extensions;

using System.Collections;

namespace ScheduledMeets.Internals.Collections;

public class Intervals<T> : IEnumerable<Interval<T>> where T : IComparable<T>
{
    private readonly IntervalTree<T, Interval<T>> _tree;

    public int Count => _tree.Count;
    public bool IsReadOnly => false;

    public Intervals() => _tree = new();
    public Intervals(IEnumerable<Interval<T>> intervals)
    {
        _tree = new();
        foreach (Interval<T> interval in intervals)
            _tree.Add(interval.From, interval.To, interval);
    }

    public void Union(Interval<T> interval)
    {
        IReadOnlyList<Interval<T>> overlaps = _tree.Query(interval.From, interval.To)
            .Evaluate();
        if (overlaps.Count == 0)
        {
            _tree.Add(interval.From, interval.To, interval);
            return;
        }

        if (overlaps.Count == 1 && overlaps[0].Contains(interval))
            return;

        _tree.Remove(overlaps);

        Interval<T> first = overlaps.Append(interval)
            .MinBy(interval => interval.From);
        Interval<T> last = overlaps.Append(interval)
            .MaxBy(interval => interval.To);

        Interval<T> union = new(first.From, last.To);
        _tree.Add(union.From, union.To, union);
    }

    public void Clear() => _tree.Clear();

    public bool Contains(T point) => _tree.Query(point).Any();

    public IEnumerator<Interval<T>> GetEnumerator()
    {
        foreach (RangeValuePair<T, Interval<T>> item in _tree)
            yield return new(item.From, item.To);
    }

    public void Subtract(Interval<T> interval)
    {
        IReadOnlyList<Interval<T>> overlaps = _tree.Query(interval.From, interval.To)
            .Evaluate();
        if (overlaps.Count == 0)
            return;

        _tree.Remove(overlaps);

        Interval<T> first = overlaps.MinBy(interval => interval.From);
        Interval<T> last = overlaps.MaxBy(interval => interval.To);

        if (first.From.CompareTo(interval.From) < 0)
        {
            Interval<T> inte = new(first.From, interval.From);
            _tree.Add(inte.From, inte.To, inte);
        }

        if (last.To.CompareTo(interval.To) > 0)
        {
            Interval<T> inte = new(interval.To, last.To);
            _tree.Add(inte.From, inte.To, inte);
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
