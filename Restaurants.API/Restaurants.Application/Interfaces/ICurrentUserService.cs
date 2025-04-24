namespace Restaurants.Application.Interfaces
{
    public interface ICurrentUserService
    {


        string? UserName { get; }
        long? CompanyId { get; }
    }

}
