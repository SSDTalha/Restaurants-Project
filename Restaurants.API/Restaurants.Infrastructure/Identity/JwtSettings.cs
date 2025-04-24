namespace Restaurants.Infrastructure.Identity;

public class JwtSettings
{
    public int ExpiryMinutes { get; set; }
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;

}