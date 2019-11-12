using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThermoBet.Core.Models;

namespace ThermoBet.Data
{
    public class UserConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasKey(c => c.Id);

            entityTypeBuilder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            entityTypeBuilder.Property(c => c.Login)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired(true);

            entityTypeBuilder.Property(c => c.HashPassword)
                .HasMaxLength(32)
                .IsRequired(true);

            entityTypeBuilder
                .HasIndex(c => new {c.Login, c.HashPassword})
                .IsUnique(true)
                .HasFilter(null);
        }
    }
}
