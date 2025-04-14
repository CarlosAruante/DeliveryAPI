using DeliveryAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DeliveryAPI
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DeliveryDbContext>
    {
        public DeliveryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DeliveryDbContext>();

            // Carregando a string de conexão a partir do arquivo appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new DeliveryDbContext(optionsBuilder.Options);
        }
    }
}
