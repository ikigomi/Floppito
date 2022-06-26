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
    public class CategoryConfiguration: IEntityTypeConfiguration<CategoryEntity>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.ToTable(nameof(CategoryEntity).RemoveEntityFromString());

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasIndex(_ => _.Title).IsUnique();

            builder.Property(_ => _.Title)
                .HasMaxLength(60)
                .IsRequired();

            builder.HasMany(p => p.Posts)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Category_Posts");
        }
    }
}
