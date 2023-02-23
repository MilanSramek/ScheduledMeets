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

    public async ValueTask<TWithId> ReadAsync(long id, CancellationToken cancellationToken) =>
        await LoadAsync(id, cancellationToken);

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
