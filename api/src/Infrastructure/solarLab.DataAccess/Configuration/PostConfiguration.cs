using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using solarLab.Contracts.Enums;
using solarLab.Domain.Entities;
using solarLab.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.DataAccess.Configuration
{
    /// <summary>
    /// Конфигурация таблицы БД Post.
    /// </summary>
    public class PostConfiguration : IEntityTypeConfiguration<PostEntity>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<PostEntity> builder)
        {
            builder.ToTable(nameof(PostEntity).RemoveEntityFromString());

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(_ => _.Title)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(_ => _.Description)
                .HasMaxLength(2000);

            builder.Property(p => p.PostStatusId).HasConversion<int>();

            builder.Property(p=>p.Images).HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            builder.Property(p => p.WorkExperience).HasConversion(
                e => string.Join(',', e.Cast<int>().ToList()),
                e => e.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x=>int.Parse(x)).Cast<Experience>().ToList());

            builder.Property(p => p.WorkSchedule).HasConversion(
                e => string.Join(',', e.Cast<int>().ToList()),
                e => e.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).Cast<Schedule>().ToList());

            builder.HasMany(p => p.Comments)
                .WithOne(p => p.Post)
                .HasForeignKey(p => p.PostId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Post_Comments");
        }
    }
}
