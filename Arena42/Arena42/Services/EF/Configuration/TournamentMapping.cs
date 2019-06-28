using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Arena42.Models;

namespace Arena42.Services.EF.Configuration
{
    internal class TournamentMapping : EntityTypeConfiguration<Tournament>
    {
        public TournamentMapping()
        {
            ToTable("Tournament");

            HasKey(g => g.Id);

            Property(g => g.Description).IsRequired();
            Property(g => g.StartTimeUtc).IsRequired();
            Property(g => g.EndTimeUtc).IsRequired();
            Property(g => g.ImgUrl).IsRequired();
            Property(g => g.Name).IsRequired();

            //Ignore(x => x.Market);
            HasMany(x => x.Markets)
                .WithMany(y => y.Tournaments)
                .Map(x =>
                {
                    x.MapLeftKey("TournamentId");
                    x.MapRightKey("MarketId");
                    x.ToTable("TournamentMarket");
                });

            HasMany(x => x.Bet)
                .WithRequired(x => x.Tournament)
                .Map(x => x.MapKey("TournamentId"));


            //HasOptional(g => g.ParentGame)
            //    .WithMany(a => a.Rounds)
            //    .HasForeignKey(x => x.ParentGameId);
        }
    }
}