namespace ScheduledMeets.Business.Interfaces;

public interface IProvider<TValue>
{
    Task<TValue> GetAsync(CancellationToken cancellationToken = default);
}
