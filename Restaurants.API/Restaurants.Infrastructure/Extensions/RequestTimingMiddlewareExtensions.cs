using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Restaurants.Infrastructure.Middlewares;

namespace Restaurants.Infrastructure.Extensions;

public static class RequestTimingMiddlewareExtensions
{

    public static IApplicationBuilder UseRequestTiming(this IApplicationBuilder app, IConfiguration configuration)
    {
        var isEnabled = configuration.GetValue<bool>("EnableRequestLogging", true);
        return app.UseMiddleware<RequestTimingMiddleware>(isEnabled);
    }
    public static IApplicationBuilder UseRequestTiming(this IApplicationBuilder app, bool isEnabled = true)
    {
        return app.UseMiddleware<RequestTimingMiddleware>(isEnabled);
    }
}
