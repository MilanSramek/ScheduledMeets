using Dawn;

using Microsoft.EntityFrameworkCore;

namespace ScheduledMeets.Persistance.Configurations;

static class ApplyingCofigurations
{
    public static ModelBuilder ApplyConfigurations(this ModelBuilder modelBuilder) =>
        Guard.Argument(modelBuilder, nameof(modelBuilder)).NotNull().Value
            .ApplyConfiguration(new UserConfiguration());
}
