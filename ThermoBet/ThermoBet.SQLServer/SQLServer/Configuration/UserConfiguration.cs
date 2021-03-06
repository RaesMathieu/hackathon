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

            entityTypeBuilder.Property(c => c.Pseudo)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired(false);

            entityTypeBuilder.Property(c => c.Avatar)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired(false);

            entityTypeBuilder.Property(c => c.BetclicUserName)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired(false);

            entityTypeBuilder.Property(c => c.Email)
                .HasMaxLength(255)
                .IsUnicode()
                .IsRequired(false);

            entityTypeBuilder.Property(c => c.FirstName)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired(false);

            entityTypeBuilder.Property(c => c.SecondName)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired(false);

            entityTypeBuilder.Property(c => c.HashPassword)
                .HasMaxLength(32)
                .IsRequired(true);

            entityTypeBuilder
                .HasIndex(c => c.Login)
                .IsUnique(true)
                .HasFilter(null);

            entityTypeBuilder
                .HasIndex(c => c.Pseudo)
                .IsUnique(true)
                .HasFilter(null);

            entityTypeBuilder
                .HasMany(c => c.LoginDate)
                .WithOne(c => c.User);

        }
    }
}
