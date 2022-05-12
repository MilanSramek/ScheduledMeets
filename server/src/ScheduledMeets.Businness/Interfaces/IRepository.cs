using ScheduledMeets.Core;

namespace ScheduledMeets.Business.Interfaces;

public interface IRepository<TEntity> : IReadRepository<TEntity> where TEntity : IEntity
{
    public Task SaveAsync(TEntity entity, CancellationToken cancellationToken = default);
    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}
