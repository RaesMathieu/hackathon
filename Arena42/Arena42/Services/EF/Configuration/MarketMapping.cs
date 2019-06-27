﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Arena42.Models;

namespace Arena42.Services.EF.Configuration
{
    internal class MarketMapping : EntityTypeConfiguration<Market>
    {
        public MarketMapping()
        {
            ToTable("Market");

            HasKey(g => g.Id);

            Property(g => g.ImgUrl).IsRequired();
            Property(g => g.Name).IsRequired();

            Ignore(x => x.Selections);

            HasMany(x => x.Tournaments)
                .WithMany(y => y.Market)
                .Map(x =>
                {
                    x.MapRightKey("TournamentId");
                    x.MapLeftKey("MarketId");
                    x.ToTable("TournamentMarket");
                });

            //HasOptional(g => g.ParentGame)
            //    .WithMany(a => a.Rounds)
            //    .HasForeignKey(x => x.ParentGameId);
        }
    }
}