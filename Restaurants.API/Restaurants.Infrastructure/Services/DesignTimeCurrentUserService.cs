using Restaurants.Application.Interfaces;

namespace Restaurants.Infrastructure.Services
{
    public class DesignTimeCurrentUserService : ICurrentUserService
    {
        public string? UserName => "migration-user";
        public long? CompanyId => 1; // ya koi default dummy CompanyId
    }
}
