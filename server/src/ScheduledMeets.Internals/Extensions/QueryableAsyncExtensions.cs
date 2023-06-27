using Microsoft.EntityFrameworkCore;

using ScheduledMeets.Internals.Reflection;

using System.Linq.Expressions;

namespace ScheduledMeets.Internals.Extensions;

public static class QueryableAsyncExtensions
{
    public static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(
        this IQueryable<TSource> queryable,
        Func<TSource, TKey> keySelector,
        CancellationToken cancellationToken = default) where TKey : notnull
    {
        return EntityFrameworkQueryableExtensions
            .ToDictionaryAsync(queryable, keySelector, cancellationToken);
    }

    public static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(
       this IQueryable<TSource> queryable,
       Func<TSource, TKey> keySelector,
       Func<TSource, TElement> elementSelector,
       CancellationToken cancellationToken = default) where TKey : notnull
    {
        return EntityFrameworkQueryableExtensions
            .ToDictionaryAsync(queryable, keySelector, elementSelector, cancellationToken);
    }

    public static async Task<Lookup<TKey, TSource>> ToLookupAsync<TSource, TKey>(
        this IQueryable<TSource> queryable,
        Expression<Func<TSource, TKey>> keySelector,
        CancellationToken cancellationToken = default) where TKey : notnull
    {
        ParameterExpression parameter = keySelector.Parameters[0];
        Expression body = Expression.New(
            Constructor.Tuple<TKey, TSource>(),
            keySelector.Body,
            parameter);

        Expression<Func<TSource, Tuple<TKey, TSource>>> selector = Expression
            .Lambda<Func<TSource, Tuple<TKey, TSource>>>(body, parameter);

        List<Tuple<TKey, TSource>> result = await queryable
             .Select(selector)
             .ToListAsync(cancellationToken);

        return (Lookup<TKey, TSource>)result.ToLookup(
            _ => _.Item1,
            _ => _.Item2);
    }

    public static Task<List<TSource>> ToListAsync<TSource>(
        this IQueryable<TSource> queryable,
        CancellationToken cancellationToken = default)
    {
        return EntityFrameworkQueryableExtensions.ToListAsync(queryable, cancellationToken);
    }

    public static Task<TSource?> SingleOrDefaultAsync<TSource>(
        this IQueryable<TSource> source,
        Expression<Func<TSource, bool>> predicate, 
        CancellationToken cancellationToken = default)
    {
        return EntityFrameworkQueryableExtensions
            .SingleOrDefaultAsync(source, predicate, cancellationToken);
    }

    public static Task<TSource> SingleAsync<TSource>(
       this IQueryable<TSource> source,
       Expression<Func<TSource, bool>> predicate,
       CancellationToken cancellationToken = default)
    {
        return EntityFrameworkQueryableExtensions
            .SingleAsync(source, predicate, cancellationToken);
    }
}
