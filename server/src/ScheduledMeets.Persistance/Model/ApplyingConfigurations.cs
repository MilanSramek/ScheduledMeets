using Microsoft.EntityFrameworkCore;

namespace ScheduledMeets.Persistence.Model;

static class ApplyingConfigurations
{
    public static ModelBuilder ApplyConfigurations(this ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        return modelBuilder
            .ApplyConfiguration(new UserConfiguration())
            .ApplyConfiguration(new MemberConfiguration())
            .ApplyConfiguration(new MeetsConfiguration())
            .ApplyConfiguration(new MeetConfiguration())
            .ApplyConfiguration(new AttendanceConfiguration());
    }
}
