using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping.EF;

using Microsoft.EntityFrameworkCore;

using ScheduledMeets.Business.Interfaces;
using ScheduledMeets.Core;
using ScheduledMeets.Persistance.Helpers;

using System.Collections;
using System.Linq.Expressions;

namespace ScheduledMeets.Persistance;

internal class ReadRepository<TEntity, TModel> : IReadRepository<TEntity> 
    where TEntity : IEntity
    where TModel : class
{
    private static ConvertRemover<TEntity> _convertRemover = new();
    private readonly IQueryable<TEntity> _base;

    public ReadRepository(AccessContext context, IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(context);
        _base = context.Set<TModel>().AsNoTrackingWithIdentityResolution()
            .UseAsAsyncDataSource(mapper)
            .BeforeProjection(_convertRemover)
            .For<TEntity>();
    }

    public Type ElementType => _base.ElementType;
    public Expression Expression => _base.Expression;
    public IQueryProvider Provider => _base.Provider;

    public IAsyncEnumerator<TEntity> GetAsyncEnumerator(
        CancellationToken cancellationToken = default)
    {
        return _base.AsAsyncEnumerable().GetAsyncEnumerator(cancellationToken);
    }

    public IEnumerator<TEntity> GetEnumerator() => _base.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => (_base as IEnumerable).GetEnumerator();
}
