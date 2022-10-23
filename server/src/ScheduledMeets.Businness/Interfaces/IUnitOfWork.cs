namespace ScheduledMeets.Business.Interfaces;

public interface IUnitOfWork
{
    Task SaveChanges(CancellationToken cancellationToken = default);
}