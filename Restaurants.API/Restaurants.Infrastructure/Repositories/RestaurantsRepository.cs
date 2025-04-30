using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Specification;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Specification;

namespace Restaurants.Infrastructure.Repositories;

public class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
{
    public async Task<int> Create(Restaurant entity)
    {
        dbContext.Restaurants.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task Delete(Restaurant entity)
    {
        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();
    }
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restaurants.ToListAsync();
        return restaurants;
    }

    public async Task<Restaurant> GetByIdAsync(int id)
    {
        var restaurant = await dbContext.Restaurants
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(x => x.Id == id);

        return restaurant ?? new Restaurant();    //default value return ho rae hai
    }



    public async Task<Restaurant?> GetBySpecificationAsync(ISpecificationo<Restaurant> specification)
    {
        var query = SpecificationEvaluator.GetQuery(dbContext.Restaurants.AsQueryable(), specification);
        return await query.FirstOrDefaultAsync();
    }




    public Task Savechanges()
        => dbContext.SaveChangesAsync();

}

