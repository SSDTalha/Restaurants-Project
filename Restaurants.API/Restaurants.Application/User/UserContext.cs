﻿using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Restaurants.Application.User;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = httpContextAccessor?.HttpContext?.User;
        if (user == null)
            throw new UnauthorizedAccessException("User context is not present");
        if (!user.Identity.IsAuthenticated || user.Identity == null)
        { return null; }
        var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value);
        return new CurrentUser(userId, email, roles);
    }
}
