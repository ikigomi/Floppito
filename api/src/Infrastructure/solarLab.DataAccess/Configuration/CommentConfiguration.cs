using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
    /// Конфигурация таблицы БД Comments.
    /// </summary>
    public class CommentConfiguration : IEntityTypeConfiguration<CommentEntity>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<CommentEntity> builder)
        {
            builder.ToTable(nameof(CommentEntity).RemoveEntityFromString());

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(_ => _.CommentBody)
                .HasMaxLength(1000)
                .IsRequired();
        }
    }
}
