using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScheduledMeets.Persistance.Model;

internal class AttendeeConfigurationn : IEntityTypeConfiguration<Attendee>
{
    public void Configure(EntityTypeBuilder<Attendee> builder)
    {
        builder.HasKey(_ => _.Id);

        builder
            .HasOne(_ => _.User)
            .WithMany(_ => _.Attendees)
            .IsRequired();
        builder.HasIndex(_ => _.UserId);

        builder
            .HasOne(_ => _.Meets)
            .WithMany(_ => _.Attendees)
            .IsRequired();
        builder.HasIndex(_ => _.MeetsId);
    }
}
