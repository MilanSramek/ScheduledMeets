using Dawn;

using Microsoft.EntityFrameworkCore;

using ScheduledMeets.Business.Interfaces;
using ScheduledMeets.Core;

using System.Collections;
using System.Linq.Expressions;

namespace ScheduledMeets.Persistance;

class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    private readonly DbSet<TEntity> _set;

    public Repository(AccessContext context)
    {
        Guard.Argument(context, nameof(context)).NotNull();
        _set = context.Set<TEntity>();
    }

    #region Reads

    public Type ElementType => (_set as IQueryable<TEntity>).ElementType;
    public Expression Expression => (_set as IQueryable<TEntity>).Expression;
    public IQueryProvider Provider => (_set as IQueryable<TEntity>).Provider;
    IEnumerator IEnumerable.GetEnumerator() => (_set as IEnumerable).GetEnumerator();
    public IEnumerator<TEntity> GetEnumerator() => (_set as IEnumerable<TEntity>).GetEnumerator();
    public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken) =>
        _set.GetAsyncEnumerator(cancellationToken);

    #endregion Reads

    #region Writes

    public async Task SaveAsync(TEntity entity, CancellationToken cancellationToken)
    {
        Guard.Argument(entity, nameof(entity)).NotNull();

        if (entity.Id > 0)
        {
            _set.Update(entity).DetectChanges();
            return;
        }

        await _set.AddAsync(entity, cancellationToken);
    }

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        Guard.Argument(entity, nameof(entity)).NotNull();

        _set.Remove(entity);
        return Task.CompletedTask;
    }

    #endregion Writes
}
