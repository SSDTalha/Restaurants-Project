using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
    public interface IDapperRepository
    {

        Task<IEnumerable<Restaurant>> GetAllAsync();
    }
}
