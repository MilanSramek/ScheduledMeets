namespace ScheduledMeets.Internals;

public static class Math
{
    public static T Min<T>(T a, T b) where T : IComparable<T> => a.CompareTo(b) <= 0 ? a : b;
    public static T Max<T>(T a, T b) where T : IComparable<T> => a.CompareTo(b) >= 0 ? a : b;
}
