using Restaurants.Application.Interfaces;

namespace Restaurants.Infrastructure.Services
{
    public class DesignTimeCurrentUserService : ICurrentUserService
    {
        public string? UserName => "migration-user";
        public string? CompanyId { get; private set; } = "1"; // ya koi default dummy CompanyId

        // Implementing the interface method
        public void SetCompanyId(string companyId)
        {
            CompanyId = companyId;
        }
    }
}
