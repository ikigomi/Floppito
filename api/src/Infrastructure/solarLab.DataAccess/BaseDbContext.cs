using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using solarLab.DataAccess.Configuration;
using solarLab.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.DataAccess
{
    /// <summary>
    /// Базовый контекст приложения.
    /// </summary>
    public class BaseDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguration());

            base.OnModelCreating(modelBuilder);

            //автозаполнение данных для разработки
            modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole
            {
                Id = new Guid("9161399F-105D-4372-87AE-CA232BA7CF36"),
                Name = Contracts.Enums.Roles.Admin.ToString(),
                NormalizedName= Contracts.Enums.Roles.Admin.ToString().ToUpper()
            });

            modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole
            {
                Id = new Guid("0F2652F5-20C6-45D1-9D46-C533F9B85576"),
                Name = Contracts.Enums.Roles.User.ToString(),
                NormalizedName = Contracts.Enums.Roles.User.ToString().ToUpper()
            });           
        }
    }
}
