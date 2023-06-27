using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScheduledMeets.Persistence.Model;

internal sealed class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
{
    public void Configure(EntityTypeBuilder<Attendance> builder)
    {
        builder.HasKey(_ => _.Id);

        builder
            .HasOne(_ => _.Member)
            .WithMany(_ => _.Attendances)
            .IsRequired();
        builder.HasIndex(_ => _.MemberId);

        builder
            .HasOne(_ => _.Meet)
            .WithMany(_ => _.Attendances)
            .IsRequired();
        builder.HasIndex(_ => _.MeetId);

        builder.HasAlternateKey(_ => new
        {
            _.MemberId,
            _.MeetId
        });
    }
}
