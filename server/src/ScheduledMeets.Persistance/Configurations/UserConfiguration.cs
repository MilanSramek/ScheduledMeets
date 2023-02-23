using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ScheduledMeets.Core;

namespace ScheduledMeets.Persistance.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
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

        builder.OwnsOne(_ => _.Name);
        builder
            .Navigation(_ => _.Name)
            .IsRequired();

        builder.Property(_ => _.Nickname);
        builder.Property(_ => _.Email);
    }
}
