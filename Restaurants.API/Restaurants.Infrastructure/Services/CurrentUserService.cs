using Microsoft.AspNetCore.Http;
using Restaurants.Application.Interfaces;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    public string? CompanyId { get; private set; }
    public string? UserName { get; private set; }
    //private readonly IHttpContextAccessor _httpContextAccessor;
    //public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    //{
    //    _httpContextAccessor = httpContextAccessor;
    //}

    //public string? UserName => _httpContextAccessor.HttpContext?.User?.Identity?.Name;

    //public long? CompanyId
    //{
    //    get
    //    {
    //        var claim = _httpContextAccessor.HttpContext?.User?.FindFirst("CompanyId")?.Value;
    //        return long.TryParse(claim, out var id) ? id : null;
    //    }
    //}
    //public void SetUserId(long userId)
    //{
    //    UseriId = userId;
    //}
    public void SetCompanyId(string companyId)
    {
        CompanyId = companyId;
  }

}