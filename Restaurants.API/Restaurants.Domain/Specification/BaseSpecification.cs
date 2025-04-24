using Restaurants.Domain.Repositories;

namespace Restaurants.Domain.Specification;

// Add the generic type parameter <T> to the class definition to fix the CS0246 error.
public abstract class BaseSpecification<T> : ISpecification<T>
{
    public abstract string ToWhereClause();

    public abstract object GetParameters();

    protected string CombineWithAnd(params string[] conditions)
    {
        return string.Join(" AND ", conditions.Where(c => !string.IsNullOrEmpty(c)));
    }
}

