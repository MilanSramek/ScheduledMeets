namespace ScheduledMeets.View;

public interface IWithIdReader<TView> where TView : IWithId
{
    Task<TView> ReadAsync(long id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TView>> ReadAsync(IReadOnlyCollection<long> ids,
        CancellationToken cancellationToken);
}
