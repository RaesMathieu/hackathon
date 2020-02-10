using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThermoBet.Core.Models;

namespace ThermoBet.Data
{
    public class TournamentConfiguration : IEntityTypeConfiguration<TournamentModel>
    {
        public void Configure(EntityTypeBuilder<TournamentModel> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasKey(c => c.Id);

            entityTypeBuilder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            entityTypeBuilder.Property(c => c.Description)
                .HasMaxLength(2500);

            entityTypeBuilder.Property(c => c.Name)
                .HasMaxLength(250)
                .IsRequired(true);

            entityTypeBuilder.Property(c => c.Code)
                .HasMaxLength(50)
                .IsRequired(true);

            entityTypeBuilder.Property(c => c.EndTimeUtc)
                .IsRequired(true);

            entityTypeBuilder.Property(c => c.StartTimeUtc)
                .IsRequired(true);

            entityTypeBuilder.HasMany(c => c.Markets)
                .WithOne(c => c.Tournament);

            entityTypeBuilder.HasMany(c => c.Winnables)
                .WithOne(c => c.Tournament);
        }
    }
}
