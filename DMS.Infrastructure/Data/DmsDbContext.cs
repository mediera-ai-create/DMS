using Microsoft.EntityFrameworkCore;
using DMS.Models.Entities;

namespace DMS.Infrastructure.Data
{
    public class DmsDbContext : DbContext
    {
        public DmsDbContext(DbContextOptions<DmsDbContext> options) : base(options) { }

        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<ProductMovement> ProductMovements { get; set; }
        public DbSet<ServiceAppointment> ServiceAppointments { get; set; }
        public DbSet<JobCard> JobCards { get; set; }
        public DbSet<WarrantyClaim> WarrantyClaims { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Dealer>().ToTable("Dealers");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Sale>().ToTable("Sales");
            modelBuilder.Entity<ServiceAppointment>().ToTable("ServiceAppointments");
            modelBuilder.Entity<JobCard>().ToTable("JobCards");

            modelBuilder.Entity<ServiceAppointment>(entity =>
            {
                entity.ToTable("ServiceAppointments");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Status)
                      .IsRequired();

                entity.Property(e => e.ScheduledDate)
                      .IsRequired();

                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<JobCard>(entity =>
            {
                entity.ToTable("JobCards");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.MechanicName).IsRequired();
                entity.Property(e => e.WorkDescription).IsRequired();
                entity.Property(e => e.EstimatedCost).HasColumnType("decimal(10,2)");
            });

            modelBuilder.Entity<WarrantyClaim>(entity =>
            {
                entity.ToTable("WarrantyClaims");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.IssueDescription).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.ClaimDate).IsRequired();
            });


            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Product)
                .WithMany() // A Product can have many Sales
                .HasForeignKey(s => s.ProductId);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Dealer)
                .WithMany() // If a Dealer can have many Sales
                .HasForeignKey(s => s.DealerId);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Customer)
                .WithMany() // If a Customer can make many Sales
                .HasForeignKey(s => s.CustomerId);

            modelBuilder.Entity<ProductMovement>()
                .HasOne(pm => pm.Product)
                .WithMany()
                .HasForeignKey(pm => pm.ProductId);

            modelBuilder.Entity<Lead>(entity =>
            {
                entity.ToTable("Leads");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Source).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.FollowUpDate).IsRequired();

                entity.HasOne(e => e.Customer)
                      .WithMany(c => c.Leads)
                      .HasForeignKey(e => e.CustomerId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.ToTable("Campaigns");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Channel).IsRequired();
                entity.Property(e => e.Status).IsRequired();
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedbacks");
                entity.HasKey(f => f.Id);
                entity.HasOne(f => f.Dealer).WithMany().HasForeignKey(f => f.DealerId);
                entity.HasOne(f => f.Customer).WithMany().HasForeignKey(f => f.CustomerId);
            });


        }
    }
}
