using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ScheduledMeets.Core;

namespace ScheduledMeets.Persistance.Configurations;

class MeetsConfiguration : IEntityTypeConfiguration<Meets>
{
    public void Configure(EntityTypeBuilder<Meets> builder)
    {
        builder.HasKey(_ => _.Id);



        builder.Property(_ => _.Name);
    }
}
