using Microsoft.OpenApi.Models;

namespace Restaurants.API.Extensions;

public static class WebApplicationBuilderExtensions
{

    public static void AddPresentaiton(this WebApplicationBuilder builder)
    {
        //builder.Services.AddAuthentication();
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen(c =>
        {
            //c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

            //Add JWT Bearer configuration
            c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearerAuth"
                }
            },
            []
        }
    });


        });
        builder.Services.AddEndpointsApiExplorer();
    }
}
