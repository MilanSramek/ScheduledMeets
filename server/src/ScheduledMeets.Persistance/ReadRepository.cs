﻿using Microsoft.EntityFrameworkCore;

using ScheduledMeets.Business.Interfaces;
using ScheduledMeets.Core;

using System.Collections;
using System.Linq.Expressions;

namespace ScheduledMeets.Persistance;

internal class ReadRepository<TEntity> : IReadRepository<TEntity> where TEntity : class, IEntity
{
    private readonly IQueryable<TEntity> _base;

    public ReadRepository(AccessContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _base = context.Set<TEntity>();
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
