using Microsoft.EntityFrameworkCore.Query;

using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace ScheduledMeets.TestTools.AsyncQueryables;

internal class TestAsyncQueryable : IQueryable
{
    public class QueryProvider : IAsyncQueryProvider
    {
        private static MethodInfo? _executeTaskInfo;
        private static MethodInfo? _executeEnumInfo;

        private static MethodInfo ExecuteTaskInfo => _executeTaskInfo
            ??= ExtractMethodCallInfo((p, e) => p.ExecuteAsTask<object>(e));

        private static MethodInfo ExecuteEnumInfo => _executeEnumInfo
           ??= ExtractMethodCallInfo((p, e) => p.ExecuteAsEnumeration<object>(e));

        private readonly IQueryable _queryable;

        public QueryProvider(IQueryable queryable)
        {
            _queryable = queryable ?? throw new ArgumentNullException(nameof(queryable));
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new TestAsyncQueryable(_queryable.Provider.CreateQuery(expression));
        }

        public IQueryable<TResult> CreateQuery<TResult>(Expression expression)
        {
            return new TestAsyncQueryable<TResult>(_queryable.Provider.CreateQuery<TResult>(expression));
        }

        public object? Execute(Expression expression)
        {
            return _queryable.Provider.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _queryable.Provider.Execute<TResult>(expression);
        }

        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken _)
        {
            Type resultGenericType = typeof(TResult).GetGenericTypeDefinition();

            if (resultGenericType.IsAssignableTo(typeof(Task<>)))
            {
                Type argumentType = typeof(TResult).GetGenericArguments()[0];
                MethodInfo executeInfo = ExecuteTaskInfo.MakeGenericMethod(argumentType);

                return (TResult)executeInfo.Invoke(this, new[] { expression })!;
            }

            if (resultGenericType.IsAssignableTo(typeof(IAsyncEnumerable<>)))
            {
                Type argumentType = typeof(TResult).GetGenericArguments()[0];
                MethodInfo executeInfo = ExecuteEnumInfo.MakeGenericMethod(argumentType);

                return (TResult)executeInfo.Invoke(this, new[] { expression })!;
            }

            throw new InvalidOperationException();
        }

        private Task<TResult> ExecuteAsTask<TResult>(Expression expression)
        {
            TResult result = Execute<TResult>(expression);
            return Task.FromResult(result);
        }
        private IAsyncEnumerable<TResult> ExecuteAsEnumeration<TResult>(Expression expression)
        {
            IEnumerable<TResult> result = Execute<IEnumerable<TResult>>(expression);
            return new TestAsyncEnumerable<TResult>(result);
        }

        private static MethodInfo ExtractMethodCallInfo(Expression<Func<QueryProvider, Expression, object>> expression)
        {
            return ((MethodCallExpression)expression.Body).Method
                .GetGenericMethodDefinition();
        }
    }

    protected readonly IQueryable _querable;

    public TestAsyncQueryable(IQueryable querable)
    {
        _querable = querable ?? throw new ArgumentNullException(nameof(querable));
        Provider = new QueryProvider(querable);
    }

    public Type ElementType => _querable.ElementType;
    public Expression Expression => _querable.Expression;
    public IQueryProvider Provider { get; }

    IEnumerator IEnumerable.GetEnumerator() => _querable.GetEnumerator();
}

internal class TestAsyncQueryable<TElement> : TestAsyncQueryable, IQueryable<TElement>
{
    public TestAsyncQueryable(IQueryable<TElement> querable) : base(querable)
    {
    }

    public IEnumerator<TElement> GetEnumerator()
    {
        IEnumerator enumerator = _querable.GetEnumerator();
        while (enumerator.MoveNext())
        {
            yield return (TElement)enumerator.Current;
        }
    }
}