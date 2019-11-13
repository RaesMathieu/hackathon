using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThermoBet.Core.Models;

namespace ThermoBet.Data
{
    public class SelectionConfiguration : IEntityTypeConfiguration<SelectionModel>
    {
        public void Configure(EntityTypeBuilder<SelectionModel> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasKey(c => c.Id);

            entityTypeBuilder
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            entityTypeBuilder.Property(c => c.Name)
                .HasMaxLength(250)
                .IsUnicode()
                .IsRequired(true);

            entityTypeBuilder.Property(c => c.IsYes)
                .IsRequired(true);

            entityTypeBuilder.HasOne(c => c.Market)
                .WithMany(c => c.Selections);

            entityTypeBuilder
                .HasIndex("MarketId", "IsYes")
                .IsUnique(true)
                .HasFilter(null);  
        }
    }
}
