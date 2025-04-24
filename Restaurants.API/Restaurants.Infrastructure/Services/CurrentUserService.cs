using Microsoft.AspNetCore.Http;
using Restaurants.Application.Interfaces;

namespace Restaurants.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{

    private readonly IHttpContextAccessor _httpContextAccessor;
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserName => _httpContextAccessor.HttpContext?.User?.Identity?.Name;

    public long? CompanyId
    {
        get
        {
            var claim = _httpContextAccessor.HttpContext?.User?.FindFirst("CompanyId")?.Value;
            return long.TryParse(claim, out var id) ? id : null;
        }
    }
}