using System.ComponentModel.Design;
using System.IdentityModel.Tokens.Jwt;
using Restaurants.Application.Interfaces;
using Restaurants.Domain.Entities;

namespace Restaurants.API.Middlewares;

public class TokenDecoderMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TokenDecoderMiddleware> _logger;

    public TokenDecoderMiddleware(RequestDelegate next, ILogger<TokenDecoderMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var path = context.Request.Path.Value?.ToLower();

        if (path.Contains("/login") || path.Contains("/register"))
        {
            await _next(context);
            return;
        }

        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            try
            {
                _logger.LogInformation("Token found in request header.");

                var tokenHandler = new JwtSecurityTokenHandler();

                if (!tokenHandler.CanReadToken(token))
                {
                    _logger.LogWarning("Invalid token.");
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Invalid token.");
                    return;
                }

                var jwtToken = tokenHandler.ReadJwtToken(token);

                if (jwtToken != null)
                {
                    var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;
                    var companyId = jwtToken.Claims.FirstOrDefault(x => x.Type == "companyId")?.Value;

                    // YAHAN pe scoped service uthani hai:
                    var currentUserService = context.RequestServices.GetRequiredService<ICurrentUserService>();

                    if (userId != null && companyId != null)
                    {
                        //currentUserService.SetUserId(userId);
                        currentUserService.SetCompanyId(companyId);

                        _logger.LogInformation($"Decoded UserId: {userId}, CompanyId: {companyId}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error decoding token: {ex.Message}");
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Error decoding token.");
                return;
            }
        }
        else
        {
            _logger.LogWarning("No token found in the request header.");
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("No token found in the request header.");
            return;
        }

        await _next(context);
    }
}






