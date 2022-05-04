using Microsoft.EntityFrameworkCore;

using ScheduledMeets.Core;
using ScheduledMeets.Persistance.Configurations;

namespace ScheduledMeets.Persistance;

sealed class AccessContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public AccessContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new UserConfiguration())
            .UseHiLo();
    }
}
