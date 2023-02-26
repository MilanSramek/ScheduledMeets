using ScheduledMeets.TestTools.AsyncQueryables;

namespace ScheduledMeets.TestTools.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TElement> AsAsyncQueryable<TElement>(
        this IEnumerable<TElement> source)
    {
        ArgumentNullException.ThrowIfNull(source);
        return new QueryableAsyncWrapper<TElement>(source.AsQueryable());
    }
}
