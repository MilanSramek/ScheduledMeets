namespace ScheduledMeets.View;

public interface IWithIdReader<TView> where TView : IWithId
{
    ValueTask<TView> ReadAsync(long id, CancellationToken cancellationToken);
}
