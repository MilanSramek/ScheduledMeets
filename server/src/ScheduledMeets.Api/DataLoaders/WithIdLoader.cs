using ScheduledMeets.Internals.Extensions;
using ScheduledMeets.View;

namespace ScheduledMeets.Api.DataLoaders;

internal class WithIdLoader<TWithId> : BatchDataLoader<long, TWithId>, IWithIdReader<TWithId>
    where TWithId : IWithId
{
    private readonly IReader<TWithId> _reader;

    public WithIdLoader(
        IReader<TWithId> reader,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _reader = reader ?? throw new ArgumentNullException(nameof(reader));
    }

    public Task<TWithId> ReadAsync(long id, CancellationToken cancellationToken) =>
        LoadAsync(id, cancellationToken);

    public Task<IReadOnlyList<TWithId>> ReadAsync(IReadOnlyCollection<long> ids,
        CancellationToken cancellationToken)
    {
        return LoadAsync(ids, cancellationToken);
    }

    protected override async Task<IReadOnlyDictionary<long, TWithId>> LoadBatchAsync(
        IReadOnlyList<long> keys,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(keys);
        return await _reader
            .Where(_ => keys.Contains(_.Id))
            .ToDictionaryAsync(_ => _.Id, cancellationToken);
    }
}
