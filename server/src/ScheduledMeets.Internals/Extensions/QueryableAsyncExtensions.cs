using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace ScheduledMeets.Internals.Extensions
{
    public static class QueryableAsyncExtensions
    {
        public static Task<List<TElement>> ToListAsync<TElement>(
            this IQueryable<TElement> queryable)
        {
            return EntityFrameworkQueryableExtensions.ToListAsync(queryable);
        }

        public static Task<TElement?> SingleOrDefaultAsync<TElement>(
            this IQueryable<TElement> source,
            Expression<Func<TElement, bool>> predicate, 
            CancellationToken cancellationToken = default)
        {
            return EntityFrameworkQueryableExtensions
                .SingleOrDefaultAsync(source, predicate, cancellationToken);
        }
    }
}
