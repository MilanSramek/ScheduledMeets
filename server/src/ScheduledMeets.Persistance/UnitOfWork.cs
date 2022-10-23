using ScheduledMeets.Business.Interfaces;

namespace ScheduledMeets.Persistance;

internal class UnitOfWork : IUnitOfWork
{
    private readonly AccessContext _context;

    public UnitOfWork(AccessContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task SaveChanges(CancellationToken cancellationToken = default) => 
        _context.SaveChangesAsync(cancellationToken);
}
