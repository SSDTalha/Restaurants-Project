namespace Restaurants.Application.Interfaces
{
    public interface ICurrentUserService
    {


        string? UserName { get; }
        string? CompanyId { get; }

        //void SetUserId(string userId);
        void SetCompanyId(string companyId);
    }

}
