using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Interfaces;
using Restaurants.Application.User;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Infrastructure.Services;


namespace Restaurants.Infrastructure.Extensions
{

    // Ye dependency injection ka container hai
    public static class ServiceCollectionExtensions
    {
        public static void AddInfraStructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RestaurantsDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            var connStr = configuration.GetConnectionString("DefaultConnection");

            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            services.AddScoped<SqlConnectionFactory>();
            services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
            services.AddScoped<IDapperRepository, RestaurantDapperRepository>();
            services.AddScoped<IGenericRepository<Restaurant>, RestaurantDapperRepository>();
            services.AddScoped<IUserContext, UserContext>();
            //services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
            services.AddIdentityApiEndpoints<User>()
                .AddEntityFrameworkStores<RestaurantsDbContext>();

            //         services.AddIdentity<User, IdentityRole>()
            //.AddEntityFrameworkStores<RestaurantsDbContext>().AddDefaultTokenProviders();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();
        }
    }
}
