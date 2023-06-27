using AutoMapper;

using Microsoft.EntityFrameworkCore;

using ScheduledMeets.Business.Interfaces;
using ScheduledMeets.Core;

namespace ScheduledMeets.Persistence;

internal class Repository<TEntity, TModel> : ReadRepository<TEntity, TModel>,
    IRepository<TEntity>,
    IReadRepository<TEntity>
    where TEntity : IEntity
    where TModel : class
{
    private readonly DbSet<TModel> _set;
    private readonly IMapper _mapper;

    public Repository(
        AccessContext context,
        IMapper mapper)
        : base(context, mapper)
    {
        ArgumentNullException.ThrowIfNull(context);
        _set = context.Set<TModel>();
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    #region Writes

    public async ValueTask SaveAsync(TEntity entity, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(entity);
        TModel model = _mapper.Map<TModel>(entity);

        if (entity.Id > 0)
        {
            _set.Update(model).DetectChanges();
            return;
        }

        await _set.AddAsync(model, cancellationToken);
    }

    public ValueTask DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(entity);
        TModel model = _mapper.Map<TModel>(entity);

        _set.Remove(model);
        return ValueTask.CompletedTask;
    }

    #endregion Writes
}
