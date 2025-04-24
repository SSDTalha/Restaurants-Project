using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;




namespace Restaurants.Infrastructure.Middlewares;

public class RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger, bool isEnabled = true)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<RequestTimingMiddleware> _logger = logger;
    private bool _isEnabled = isEnabled;

    public async Task InvokeAsync(HttpContext context)
    {
        if (!_isEnabled)
        {


            await _next(context);
            return;
        }
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation("User: {User} requested {Method} Request {Path} responded {StatusCode} in {ElapsedMilliseconds} ms",
               context.User.Identity?.Name ?? "Talha",
               context.Request.Method,
               context.Request.Path,
               context.Response.StatusCode,
               stopwatch.ElapsedMilliseconds);
        }

    }


}
