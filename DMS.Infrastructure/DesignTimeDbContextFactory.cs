using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DMS.Infrastructure.Data;

namespace DMS.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DmsDbContext>
    {
        public DmsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DmsDbContext>();
            optionsBuilder.UseSqlite("Data Source=dms.db");

            return new DmsDbContext(optionsBuilder.Options);
        }
    }
}
