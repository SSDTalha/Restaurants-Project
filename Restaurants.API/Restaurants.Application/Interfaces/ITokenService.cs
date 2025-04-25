namespace Restaurants.Application.Interfaces
{
    public interface ITokenService
    {
        string GetToken(User user);
    }
}
