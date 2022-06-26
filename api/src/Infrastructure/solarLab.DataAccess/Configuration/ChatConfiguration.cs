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
    public class ChatConfiguration : IEntityTypeConfiguration<ChatEntity>
    {
        public void Configure(EntityTypeBuilder<ChatEntity> builder)
        {
            builder.ToTable(nameof(ChatEntity).RemoveEntityFromString());

            builder.HasKey(c => c.Id);
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();

            builder.HasMany(_ => _.Messages)
                    .WithOne(_=>_.Chat)
                    .HasForeignKey(_=>_.ChatId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Chat_Messages");
        }
    }
}
