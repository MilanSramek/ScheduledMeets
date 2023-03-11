using Microsoft.EntityFrameworkCore;

namespace ScheduledMeets.Persistance.Model;

static class ApplyingConfigurations
{
    public static ModelBuilder ApplyConfigurations(this ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        return modelBuilder
            .ApplyConfiguration(new UserConfiguration())
            .ApplyConfiguration(new AttendeeConfigurationn())
            .ApplyConfiguration(new MeetsConfiguration());
    }
}
