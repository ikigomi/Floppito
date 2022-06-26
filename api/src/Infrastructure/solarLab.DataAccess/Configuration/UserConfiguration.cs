using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using solarLab.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.DataAccess.Configuration
{
    /// <summary>
    /// Конфигурация таблицы БД User.
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable(nameof(ApplicationUser));

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(_ => _.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(_ => _.UserName)
                .IsRequired();

            builder.Property(_ => _.Email)
                .IsRequired();
            builder.Property(p => p.Gender).HasConversion<int>();

            builder.Ignore(u => u.Roles);


            builder.HasMany(p => p.Comments)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.CreatorId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_User_Comments");

            builder.HasMany(p => p.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.CreatorId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_User_Posts");

            builder.HasMany(p => p.Messages)
                .WithOne(p => p.Sender)
                .HasForeignKey(p => p.SenderId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_User_Messages");

            builder.HasMany(p => p.Chats)
                .WithMany(p => p.Members)
                .UsingEntity(_ => _.ToTable("UserChats"));
        }
    }
}
