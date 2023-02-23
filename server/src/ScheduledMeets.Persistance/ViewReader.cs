using Microsoft.EntityFrameworkCore;

using ScheduledMeets.View;

using System.Collections;
using System.Linq.Expressions;

namespace ScheduledMeets.Persistance;

internal class ViewReader<TView> : IReader<TView> where TView : class
{
    private readonly IQueryable<TView> _base;

    public ViewReader(AccessContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _base = context.Set<TView>().AsNoTrackingWithIdentityResolution();
    }

    public Type ElementType => _base.ElementType;
    public Expression Expression => _base.Expression;
    public IQueryProvider Provider => _base.Provider;

    public IAsyncEnumerator<TView> GetAsyncEnumerator(
        CancellationToken cancellationToken = default)
    {
        return _base.AsAsyncEnumerable().GetAsyncEnumerator(cancellationToken);
    }

    public IEnumerator<TView> GetEnumerator() => _base.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => (_base as IEnumerable).GetEnumerator();
}
