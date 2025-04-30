namespace Restaurants.Domain.Repositories;

public interface IGenericRepository<T>
{
    Task<IEnumerable<T>> QueryAsync(ISpecification<T> spec);
}

public interface ISpecification<T> // Updated to be generic
{
    int? Id { get; }
    string ToWhereClause();
    object GetParameters();
    
}
