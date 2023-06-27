using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScheduledMeets.Persistence.Model;

internal sealed class MeetConfiguration : IEntityTypeConfiguration<Meet>
{
    public void Configure(EntityTypeBuilder<Meet> builder)
    {
        builder.HasKey(_ => _.Id);

        builder
            .HasOne(_ => _.Meets)
            .WithMany(_ => _.ContainedMeets)
            .IsRequired();
        builder.HasIndex(_ => _.MeetsId);
    }
}
