using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThermoBet.Core.Models;

namespace ThermoBet.Data
{
    public class TournamentWinnableConfiguration : IEntityTypeConfiguration<TournamentWinnableModel>
    {
        public void Configure(EntityTypeBuilder<TournamentWinnableModel> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasKey(c => c.Id);

            entityTypeBuilder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            entityTypeBuilder.Property(c => c.NbGoodAnswer)
                .IsRequired(true);

            entityTypeBuilder.Property(c => c.AmountOfWinnings)
                .IsRequired(true);
        }
    }
}
