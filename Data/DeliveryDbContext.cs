
using DeliveryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryAPI.Data
{
    public class DeliveryDbContext : DbContext
    {
        public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options) : base(options) { }

        // Construtor sem parâmetros, necessário para migrations
        public DeliveryDbContext() { }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.Vehicle)
                .WithMany(v => v.Deliveries)
                .HasForeignKey(d => d.IdVehicle);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Delivery)
                .WithMany(d => d.Orders)
                .HasForeignKey(o => o.DeliveryId);
        }

    }

}


