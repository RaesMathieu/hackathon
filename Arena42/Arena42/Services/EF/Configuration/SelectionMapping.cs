using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Arena42.Models;

namespace Arena42.Services.EF.Configuration
{
    internal class SelectionMapping : EntityTypeConfiguration<Selection>
    {
        public SelectionMapping()
        {
            ToTable("Selection");

            HasKey(g => g.Id);

            Property(g => g.ImgUrl).IsRequired();
            Property(g => g.Name).IsRequired();
            Property(g => g.Odds).IsRequired();
            Property(g => g.MarketId).IsRequired();
            Property(g => g.Result);
            Property(g => g.Position);
        }
    }
}