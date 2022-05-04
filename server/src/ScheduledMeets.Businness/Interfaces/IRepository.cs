using ScheduledMeets.Core;

namespace ScheduledMeets.Business.Interfaces;

public interface IRepository<TEntity> : IReadRepository<TEntity> where TEntity : IEntity
{
    public Task<TEntity> Save(TEntity entity);
    public Task<TEntity> Delete(int id);
}
