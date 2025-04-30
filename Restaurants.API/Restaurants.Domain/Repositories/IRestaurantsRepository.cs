using Restaurants.Domain.Entities;
using Restaurants.Domain.Specification;

namespace Restaurants.Domain.Repositories;

public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant> GetByIdAsync(int id);

    Task<Restaurant> GetBySpecificationAsync(ISpecificationo<Restaurant> specification);
    Task<int> Create(Restaurant entity);
    Task Delete(Restaurant entity);
    Task Savechanges();
}


