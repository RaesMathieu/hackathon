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

            entityTypeBuilder
                .Property(c => c.Odds)
                .HasColumnType("decimal(3,2)");

            entityTypeBuilder.Property(c => c.ImgUrl)
                .HasMaxLength(250);
            
            entityTypeBuilder.Property(c => c.Name)
                .HasMaxLength(250)
                .IsUnicode()
                .IsRequired(true);

            entityTypeBuilder.Property(c => c.Result)
                .IsRequired(false);
        }
    }
}
