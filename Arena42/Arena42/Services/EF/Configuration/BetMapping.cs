using System.Data.Entity.ModelConfiguration;
using Arena42.Models;

namespace Arena42.Services.EF.Configuration
{
    internal class BetMapping : EntityTypeConfiguration<Bet>
    {
        public BetMapping()
        {
            ToTable("Bet");

            HasKey(g => g.Id);

            Property(g => g.Date).IsRequired();
            //Property(g => g.TournamentId).IsRequired();
            //Property(g => g.MarketId).IsRequired();
            //Property(g => g.SelectionId).IsRequired();
            //Property(g => g.UserId).IsRequired();
            Property(g => g.Result);

            HasRequired(x => x.Tournament)
                .WithMany(x => x.Bet)
                .Map( x => x.MapKey("TournamentId"));

            HasRequired(x => x.Market)
                .WithMany(x => x.Bet)
                .Map(x => x.MapKey("MarketId"));

            HasRequired(x => x.Selection)
                .WithMany(x => x.Bet)
                .Map(x => x.MapKey("SelectionId"));

            HasRequired(x => x.User)
                .WithMany(x => x.Bet)
                .Map(x => x.MapKey("UserId"));


        }
    }
}