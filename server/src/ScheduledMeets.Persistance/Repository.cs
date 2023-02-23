using Microsoft.EntityFrameworkCore;

using ScheduledMeets.Business.Interfaces;
using ScheduledMeets.Core;

namespace ScheduledMeets.Persistance;

internal class Repository<TEntity> : ReadRepository<TEntity>, IRepository<TEntity>,
    IReadRepository<TEntity> where TEntity : class, IEntity
{
    private readonly DbSet<TEntity> _set;

    public Repository(AccessContext context) : base(context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _set = context.Set<TEntity>();
    }

    #region Writes

    public async ValueTask SaveAsync(TEntity entity, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if (entity.Id > 0)
        {
            _set.Update(entity).DetectChanges();
            return;
        }

        await _set.AddAsync(entity, cancellationToken);
    }

    public ValueTask DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _set.Remove(entity);
        return ValueTask.CompletedTask;
    }

    #endregion Writes
}
