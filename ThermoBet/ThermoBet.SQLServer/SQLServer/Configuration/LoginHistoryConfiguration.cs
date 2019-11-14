using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThermoBet.Core.Models;

namespace ThermoBet.Data
{
    public class LoginHistoryConfiguration : IEntityTypeConfiguration<LoginHistoryModel>
    {
        public void Configure(EntityTypeBuilder<LoginHistoryModel> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasKey(c => c.Id);

            entityTypeBuilder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            entityTypeBuilder.Property(c => c.LoginDateUtc)
                .IsRequired(true);

            entityTypeBuilder
                .HasOne(c => c.User)
                .WithMany(c => c.LoginDate);

        }
    }
}
