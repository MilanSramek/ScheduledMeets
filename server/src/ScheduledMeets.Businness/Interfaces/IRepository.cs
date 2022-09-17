using ScheduledMeets.Core;

namespace ScheduledMeets.Business.Interfaces;

public interface IRepository<TEntity> : IReadRepository<TEntity> where TEntity : IEntity
{
    public ValueTask SaveAsync(TEntity entity, CancellationToken cancellationToken = default);
    public ValueTask DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}
