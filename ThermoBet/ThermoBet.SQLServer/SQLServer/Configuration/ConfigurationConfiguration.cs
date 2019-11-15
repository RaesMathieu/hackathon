using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThermoBet.SQLServer.Models;

namespace ThermoBet.Data
{
    public class ConfigurationConfiguration : IEntityTypeConfiguration<ConfigurationModel>
    {
        public void Configure(EntityTypeBuilder<ConfigurationModel> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasKey(c => c.Key);

            entityTypeBuilder.Property(c => c.Value)
                .IsRequired(true);
        }
    }
}
