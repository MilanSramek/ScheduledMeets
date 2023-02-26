using Microsoft.EntityFrameworkCore.Query;

using System.Linq.Expressions;
using System.Reflection;

namespace ScheduledMeets.TestTools.AsyncQueryables;

class QueryProviderAsyncWrapper : IAsyncQueryProvider
{
    private readonly IQueryProvider _queryProvider;

    public QueryProviderAsyncWrapper(IQueryProvider queryProvider)
    {
        _queryProvider = queryProvider
            ?? throw new ArgumentNullException(nameof(queryProvider));
    }

    public IQueryable CreateQuery(Expression expression)
    {
        IQueryable queryable = _queryProvider.CreateQuery(expression);
        return new QueryableAsyncWrapper(queryable);
    }

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
    {
        IQueryable<TElement> queryable = _queryProvider.CreateQuery<TElement>(expression);
        return new QueryableAsyncWrapper<TElement>(queryable);
    }

    public object? Execute(Expression expression) => _queryProvider.Execute(expression);

    public TResult Execute<TResult>(Expression expression) => 
        _queryProvider.Execute<TResult>(expression);

    public TResult ExecuteAsync<TResult>(Expression expression, 
        CancellationToken cancellationToken = default)
    {
        object? value = _queryProvider.Execute(expression);

        MethodInfo genericFromResultInfo = typeof(Task)
            .GetMethod(nameof(Task.FromResult), BindingFlags.Static | BindingFlags.Public)
                ?? throw new Exception("Cannot find Task.FromResult method.");
        Type valueType = typeof(TResult).GetGenericArguments().Single();
        MethodInfo fromResultInfo = genericFromResultInfo.MakeGenericMethod(valueType);

        object result = fromResultInfo.Invoke(null, new[] { value })
            ?? throw new Exception("Method Task.FromResult result is null.");
        return (TResult)result;
    }
}
