using Microsoft.EntityFrameworkCore;
using solarLab.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace solarLab.Migrations
{
    public class MigrationDbContext : BaseDbContext
    {
        public MigrationDbContext(DbContextOptions<MigrationDbContext> options) : base(options)
        {
        }
    }
}
