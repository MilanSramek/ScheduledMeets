using Dawn;

using ScheduledMeets.TestTools.AsyncQueryables;

namespace ScheduledMeets.TestTools.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TElement> AsAsyncQueryable<TElement>(
        this IEnumerable<TElement> source)
    {
        Guard.Argument(source, nameof(source)).NotNull();
        return new QueryableAsyncWrapper<TElement>(source.AsQueryable());
    }
}
