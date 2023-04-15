using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping.EF;

using Microsoft.EntityFrameworkCore;

using ScheduledMeets.Persistance.Helpers;
using ScheduledMeets.View;

using System.Collections;
using System.Linq.Expressions;

namespace ScheduledMeets.Persistance;

internal class ViewReader<TView, TModel> : IReader<TView> where TModel : class
{
    private static ConvertRemover<TView> _convertRemover = new();
    private readonly IQueryable<TView> _base;

    public ViewReader(AccessContext context, IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(context);

        _base = context.Set<TModel>().AsNoTrackingWithIdentityResolution()
            .UseAsAsyncDataSource(mapper)
            .BeforeProjection(_convertRemover)
            .For<TView>();
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