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
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<Dimension> Dimensions { get; set; }
        public DbSet<MaterialType> MaterialTypes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<ItemAttachment> ItemAttachments { get; set; }


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

            // ItemCategory
            modelBuilder.Entity<ItemCategory>(e =>
            {
                e.ToTable("ItemCategories");
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired();
            });

            // Dimension
            modelBuilder.Entity<Dimension>(e =>
            {
                e.ToTable("Dimensions");
                e.HasKey(x => x.Id);
            });

            // MaterialType
            modelBuilder.Entity<MaterialType>(e =>
            {
                e.ToTable("MaterialTypes");
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired();
            });

            // Brand mapping (if not already)
            modelBuilder.Entity<Brand>(e =>
            {
                e.ToTable("Brands");
                e.HasKey(x => x.Id);
            });

            // Item mapping
            modelBuilder.Entity<Item>(e =>
            {
                e.ToTable("Items");
                e.HasKey(x => x.Id);

                e.HasOne(i => i.Brand).WithMany(b => b.Items).HasForeignKey(i => i.BrandId).OnDelete(DeleteBehavior.SetNull);
                e.HasOne(i => i.Category).WithMany().HasForeignKey(i => i.CategoryId).OnDelete(DeleteBehavior.SetNull);
                e.HasOne(i => i.MaterialType).WithMany().HasForeignKey(i => i.MaterialTypeId).OnDelete(DeleteBehavior.SetNull);

                e.HasOne(i => i.Dimension1).WithMany().HasForeignKey(i => i.Dimension1Id).OnDelete(DeleteBehavior.SetNull);
                e.HasOne(i => i.Dimension2).WithMany().HasForeignKey(i => i.Dimension2Id).OnDelete(DeleteBehavior.SetNull);
                e.HasOne(i => i.Dimension3).WithMany().HasForeignKey(i => i.Dimension3Id).OnDelete(DeleteBehavior.SetNull);

            });

            // ItemAttachment mapping
            modelBuilder.Entity<ItemAttachment>(entity =>
            {
                entity.ToTable("ItemAttachments");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FileName).IsRequired();
                entity.Property(e => e.FilePath).IsRequired();
                entity.HasOne(a => a.Item).WithMany(i => i.Attachments).HasForeignKey(a => a.ItemId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("Requests");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });
        }
    }
}
