using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Arena42.Models;

namespace Arena42.Services.EF.Configuration
{
    internal class UserMapping : EntityTypeConfiguration<User>
    {
        public UserMapping()
        {
            ToTable("User");
            HasKey(g => g.Id);

            Property(g => g.UserName).IsRequired();
            Property(g => g.Password).IsOptional();
        }
    }
}