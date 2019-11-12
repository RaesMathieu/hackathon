using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThermoBet.Core.Models;

namespace ThermoBet.Data
{
    public class BetConfiguration : IEntityTypeConfiguration<BetModel>
    {
        public void Configure(EntityTypeBuilder<BetModel> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Bet");

            entityTypeBuilder
                .HasKey(c => new { c.MarketId, c.TournamentId, c.UserId });

            entityTypeBuilder.HasOne(c => c.Market)
                .WithMany(c => c.Bets)
                .HasForeignKey(c => c.MarketId);

            entityTypeBuilder.HasOne(c => c.Tournament)
                .WithMany(c => c.Bets)
                .HasForeignKey(c => c.TournamentId);

            entityTypeBuilder.HasOne(c => c.User)
                .WithMany(c => c.Bets)
                .HasForeignKey(c => c.UserId);

            entityTypeBuilder.HasOne(c => c.Selection)
                .WithMany(c => c.Bets)
                .IsRequired(true);

            entityTypeBuilder.Property(c => c.DateUtc)
                .IsRequired(true);
        }
    }
}
