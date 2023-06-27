using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScheduledMeets.Persistence.Model;

internal sealed class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.HasKey(_ => _.Id);

        builder
            .HasOne(_ => _.User)
            .WithMany(_ => _.Attendees)
            .IsRequired();
        builder.HasIndex(_ => _.UserId);

        builder
            .HasOne(_ => _.Meets)
            .WithMany(_ => _.Members)
            .IsRequired();
        builder.HasIndex(_ => _.MeetsId);
    }
}
