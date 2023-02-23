using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ScheduledMeets.Core;
using ScheduledMeets.View;

namespace ScheduledMeets.Persistance.Configurations;

internal class UserViewConfiguration : IEntityTypeConfiguration<UserView>
{
    public void Configure(EntityTypeBuilder<UserView> builder)
    {
        builder.ToTable("users");
        builder.HasOne<User>().WithOne().HasForeignKey<UserView>(_ => _.Id);

        builder.Property(_ => _.Id);

        builder
            .Property(_ => _.Username).HasColumnName("username")
            .IsRequired();

        builder.Property(_ => _.FirstName).HasColumnName("name_first_name");
        builder.Property(_ => _.LastName).HasColumnName("name_last_name");
        builder.Property(_ => _.Nickname).HasColumnName("nickname");
        builder.Property(_ => _.Email).HasColumnName("email");

        builder.Ignore(_ => _.Attendees);
    }
}