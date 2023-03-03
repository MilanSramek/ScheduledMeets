using Microsoft.EntityFrameworkCore;
using ScheduledMeets.Persistance.Model;

namespace ScheduledMeets.Persistance;

sealed class AccessContext : DbContext
{
    public AccessContext(DbContextOptions options) : base(options)
    {
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurations()
            .UseHiLo();
    }
}
