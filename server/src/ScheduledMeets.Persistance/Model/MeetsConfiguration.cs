using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScheduledMeets.Persistance.Model;

class MeetsConfiguration : IEntityTypeConfiguration<Meets>
{
    public void Configure(EntityTypeBuilder<Meets> builder)
    {
        builder.HasKey(_ => _.Id);
        builder
            .Property(_ => _.Name)
            .IsRequired();
    }
}
