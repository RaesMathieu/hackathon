using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThermoBet.Core.Models;

namespace ThermoBet.Data
{
    public class TournamentUserOptinConfiguration : IEntityTypeConfiguration<TournamentUserOptinModel>
    {
        public void Configure(EntityTypeBuilder<TournamentUserOptinModel> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasKey(c => new { c.TournamentId, c.UserId });

            entityTypeBuilder.HasOne(c => c.Tournament)
                .WithMany(c => c.OptinUsers)
                .HasForeignKey(c => c.TournamentId);

            entityTypeBuilder.HasOne(c => c.User)
                .WithMany(c => c.OptinTournament)
                .HasForeignKey(c => c.UserId);

            entityTypeBuilder.Property(c => c.DateUtc)
                .IsRequired(true);
        }
    }
}
