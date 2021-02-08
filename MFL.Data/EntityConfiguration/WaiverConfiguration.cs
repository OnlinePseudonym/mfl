using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MFL.Data.Entities;

namespace MFL.Data.EntityConfiguration
{
    public class WaiverConfiguration : IEntityTypeConfiguration<WaiverTransaction>
    {
        public void Configure(EntityTypeBuilder<WaiverTransaction> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(w => w.Player);
            builder.Property(w => w.LeagueId).IsRequired();
        }
    }
}
