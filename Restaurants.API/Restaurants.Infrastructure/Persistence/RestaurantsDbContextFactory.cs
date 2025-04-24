using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Restaurants.Infrastructure.Services;

namespace Restaurants.Infrastructure.Persistence
{
    public class RestaurantsDbContextFactory : IDesignTimeDbContextFactory<RestaurantsDbContext>
    {
        public RestaurantsDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../Restaurants.API")))
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<RestaurantsDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);


            return new RestaurantsDbContext(optionsBuilder.Options, new DesignTimeCurrentUserService());
        }
    }
}
