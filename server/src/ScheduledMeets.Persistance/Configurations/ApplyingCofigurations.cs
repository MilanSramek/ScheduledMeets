using Microsoft.EntityFrameworkCore;

namespace ScheduledMeets.Persistance.Configurations;

static class ApplyingCofigurations
{
    public static ModelBuilder ApplyConfigurations(this ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        return modelBuilder
            .ApplyConfiguration(new UserConfiguration());
    }
}
