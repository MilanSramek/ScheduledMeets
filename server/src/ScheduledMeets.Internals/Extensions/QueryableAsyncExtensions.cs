using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace ScheduledMeets.Internals.Extensions
{
    public static class QueryableAsyncExtensions
    {
        public static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TKey, TElement>(
            this IQueryable<TElement> queryable,
            Func<TElement, TKey> keySelector,
            CancellationToken cancellationToken = default) where TKey : notnull
        {
            return EntityFrameworkQueryableExtensions
                .ToDictionaryAsync(queryable, keySelector, cancellationToken);
        }

        public static Task<List<TElement>> ToListAsync<TElement>(
            this IQueryable<TElement> queryable,
            CancellationToken cancellationToken = default)
        {
            return EntityFrameworkQueryableExtensions.ToListAsync(queryable, cancellationToken);
        }

        public static Task<TElement?> SingleOrDefaultAsync<TElement>(
            this IQueryable<TElement> source,
            Expression<Func<TElement, bool>> predicate, 
            CancellationToken cancellationToken = default)
        {
            return EntityFrameworkQueryableExtensions
                .SingleOrDefaultAsync(source, predicate, cancellationToken);
        }

        public static Task<TElement> SingleAsync<TElement>(
           this IQueryable<TElement> source,
           Expression<Func<TElement, bool>> predicate,
           CancellationToken cancellationToken = default)
        {
            return EntityFrameworkQueryableExtensions
                .SingleAsync(source, predicate, cancellationToken);
        }
    }
}
