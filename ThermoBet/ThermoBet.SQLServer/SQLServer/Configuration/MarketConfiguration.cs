using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThermoBet.Core.Models;

namespace ThermoBet.Data
{
    public class MarketConfiguration : IEntityTypeConfiguration<MarketModel>
    {
        public void Configure(EntityTypeBuilder<MarketModel> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasKey(c => c.Id);

            entityTypeBuilder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            entityTypeBuilder.Property(c => c.Name)
                .HasMaxLength(250)
                .IsRequired(true);

            entityTypeBuilder.Property(c => c.StartTimeUtc)
                .IsRequired(true);

            entityTypeBuilder.HasMany(c => c.Selections)
                .WithOne(c => c.Market);
        }
    }
}
