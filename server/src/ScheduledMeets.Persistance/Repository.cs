using ScheduledMeets.Business.Interfaces;
using ScheduledMeets.Core;

using System.Collections;
using System.Linq.Expressions;

namespace ScheduledMeets.Persistance;

class Repository<TEntity> : IRepository<IEntity> where TEntity : class, IEntity
{
    public Type ElementType { get; }
    public Expression Expression { get; }
    public IQueryProvider Provider { get; }

    public Task<IEntity> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerator<IEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<IEntity> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public Task<IEntity> Save(IEntity entity)
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
