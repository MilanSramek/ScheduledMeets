using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScheduledMeets.Persistence.Model;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(_ => _.Id);

        builder
            .Property(_ => _.Username)
            .IsRequired();
        builder
            .HasIndex(_ => _.Username)
            .IsUnique();
    }
}
