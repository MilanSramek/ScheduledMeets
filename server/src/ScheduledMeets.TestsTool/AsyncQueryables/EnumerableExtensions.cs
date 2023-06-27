namespace ScheduledMeets.TestTools.AsyncQueryables;

public static class EnumerableExtensions
{
    public static IQueryable<TElement> AsAsyncQueryable<TElement>(this IEnumerable<TElement> enumerable) =>
        new TestAsyncQueryable<TElement>(enumerable.AsQueryable());

    public static IAsyncEnumerable<TElement> AsAsyncEnumerable<TElement>(this IEnumerable<TElement> enumerable) =>
        new TestAsyncEnumerable<TElement>(enumerable.AsQueryable());
}
