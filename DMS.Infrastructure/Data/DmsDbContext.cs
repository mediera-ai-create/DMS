using Microsoft.EntityFrameworkCore;
using DMS.Models.Entities;

namespace DMS.Infrastructure.Data
{
    public class DmsDbContext : DbContext
    {
        public DmsDbContext(DbContextOptions<DmsDbContext> options) : base(options) {

            var connectionPath = Database.GetDbConnection().DataSource;
            File.WriteAllText("connected_db.txt", $"Connected to: {connectionPath}");
        }

        public DbSet<Dealer> Dealers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dealer>().ToTable("Dealers");
        }
    }
}
