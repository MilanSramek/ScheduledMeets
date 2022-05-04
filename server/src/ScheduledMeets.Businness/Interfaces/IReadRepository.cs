using ScheduledMeets.Core;

namespace ScheduledMeets.Business.Interfaces;

public interface IReadRepository<TEntity> : IQueryable<TEntity>, IAsyncEnumerable<TEntity>
    where TEntity : IEntity
{
}
