using ErpProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ErpProject.Controllers
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("erp"); // Postavljanje podrazumevane šeme

            modelBuilder.Entity<User>().ToTable("Users", schema: "erp");
            modelBuilder.Entity<Admin>().ToTable("Admins", schema: "erp");
            modelBuilder.Entity<Product>().ToTable("Products", schema: "erp");
            modelBuilder.Entity<ShippingAddress>().ToTable("shipping_address", schema: "erp");
            modelBuilder.Entity<Order>().ToTable("Orders", schema: "erp");
            modelBuilder.Entity<OrderItem>().ToTable("order_items", schema: "erp");


            modelBuilder.Entity<Order>()
            .Property(o => o.items_price)
            .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.total_price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.transaction_amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ShippingAddress>()
                .Property(s => s.shipping_price)
                .HasColumnType("decimal(18,2)");
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

    }

   
}
