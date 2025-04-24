using Microsoft.Extensions.DependencyInjection;

namespace Restaurants.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {

            //var applicationAssemby = typeof(ServiceCollectionExtensions).Assembly;
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssemby));

            //services.AddAutoMapper(applicationAssemby);
            //services.AddValidatorsFromAssembly(applicationAssemby)
            //    .AddFluentValidationAutoValidation();
            //services.AddScoped<IRestaurantService, RestaurantsService>();
            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
        }
    }
}