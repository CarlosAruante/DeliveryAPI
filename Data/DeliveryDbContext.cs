using DeliveryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryAPI.Data
{
    public class DeliveryDbContext : DbContext
    {
        public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options) : base(options) { }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.Vehicle)
                .WithMany(v => v.Deliveries)
                .HasForeignKey(d => d.IdVehicle)
                .HasConstraintName("FK_Delivery_Vehicle");

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Delivery)
                .WithMany(d => d.Orders)
                .HasForeignKey(o => o.DeliveryId)
                .HasConstraintName("FK_Order_Delivery");

            base.OnModelCreating(modelBuilder);
        }

    }

}


