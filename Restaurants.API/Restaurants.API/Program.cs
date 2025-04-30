using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Restaurants.API.Extensions;
using Restaurants.API.Middlewares;
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
            //builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<SqlConnection>(sp => new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
            // Restaurants.API
            builder.Services.AddQuartzJobs();





            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(GetRestaurantByIdQueryHandler).Assembly);
                //cfg.RegisterServicesFromAssembly(typeof(AnotherHandler).Assembly);
            });
            //builder.Host.UseSerilog((context, configuration) => configuration
            //.WriteTo.Console());

            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.RequireHttpsMetadata = false;
            //    options.SaveToken = true;
            //    options.TokenValidationParameters = new TokenValidationParameters

            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!)),
            //        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            //        ValidAudience = builder.Configuration["JwtSettings:Audience"],
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true
            //    };
            //});
            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            //{
            //    var tokenkey = builder.Configuration["TokenKey"] ?? throw new Exception("Toekn key not found");
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenkey)),
            //        ValidateIssuer = false,
            //        ValidateAudience = false,

            //    };
            //});
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
        //    builder.Configuration["JWT:Secret"] ?? throw new InvalidOperationException("JWT:Secret is not configured")))
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        //IssuerSigningKey = new SymmetricSecurityKey(
        //        Encoding.ASCII.GetBytes("YehKoiStrongSecretKeyHai123!YehKoiStrongSecretKeyHai123!YehKoiStrongSecretKeyHai123!YehKoiStrongSecretKeyHai123!")
        //  )
    };
});



            builder.Services.AddAuthorization();

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
            app.UseMiddleware<TokenDecoderMiddleware>();

            app.UseAuthorization();

            app.UseRequestTiming(app.Configuration);
            app.MapControllers();

            app.Run();
        }
    }
}
