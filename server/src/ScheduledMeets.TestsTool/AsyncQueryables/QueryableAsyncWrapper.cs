using Dawn;

using System.Collections;
using System.Linq.Expressions;

namespace ScheduledMeets.TestTools.AsyncQueryables;

class QueryableAsyncWrapper : IQueryable
{
    protected readonly IQueryable _queryable;

    public QueryableAsyncWrapper(IQueryable queryable)
    {
        _queryable = Guard.Argument(queryable, nameof(queryable)).NotNull().Value;
    }

    public Type ElementType => _queryable.ElementType;
    public Expression Expression => _queryable.Expression;
    public IQueryProvider Provider => new QueryProviderAsyncWrapper(_queryable.Provider);

    public IEnumerator GetEnumerator() => _queryable.GetEnumerator();
}

class QueryableAsyncWrapper<TElement> : QueryableAsyncWrapper, IQueryable<TElement>
{
    public QueryableAsyncWrapper(IQueryable<TElement> queryable) : base(queryable)
    {
    }

    IEnumerator<TElement> IEnumerable<TElement>.GetEnumerator() => 
        ((IQueryable<TElement>)_queryable).GetEnumerator();
        
}
