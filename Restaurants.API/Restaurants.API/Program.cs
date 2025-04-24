using Microsoft.Data.SqlClient;
using Restaurants.API.Extensions;
using Restaurants.Application.Extensions;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Identity;
using Restaurants.Infrastructure.Seeders;
namespace Restaurants.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


            builder.AddPresentaiton();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(RestaurantsProfile));
            builder.Services.AddApplication();
            builder.Services.AddInfraStructure(builder.Configuration);
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

            builder.Services.AddScoped<SqlConnection>(sp => new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));





            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(GetRestaurantByIdQueryHandler).Assembly);
                //cfg.RegisterServicesFromAssembly(typeof(AnotherHandler).Assembly);
            });
            //builder.Host.UseSerilog((context, configuration) => configuration
            //.WriteTo.Console());
            var app = builder.Build();

            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
            await seeder.Seed();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapGroup("api/identity").MapIdentityApi<User>();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRequestTiming(app.Configuration);
            app.MapControllers();

            app.Run();
        }
    }
}
