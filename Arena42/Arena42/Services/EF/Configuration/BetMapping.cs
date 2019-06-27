using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
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
            Property(g => g.TournamentId).IsRequired();
            Property(g => g.MarketId).IsRequired();
            Property(g => g.SelectionId).IsRequired();
            Property(g => g.UserId).IsRequired();
            Property(g => g.Result);
        }
    }
}