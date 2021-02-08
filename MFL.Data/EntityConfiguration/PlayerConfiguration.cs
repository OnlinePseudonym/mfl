using MFL.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MFL.Data.EntityConfiguration
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.NflId).HasMaxLength(50);
            builder.Property(x => x.DraftTeam).HasMaxLength(50);
            builder.Property(x => x.FullName).HasMaxLength(110);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Position).HasMaxLength(10);
            builder.Property(x => x.Team).HasMaxLength(50);
            builder.Property(x => x.Status).HasMaxLength(50);
        }
    }
}
