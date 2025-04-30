using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Specification
{
    //public class RestaurantByNameSpec(string name) : BaseSpecification<Restaurant>
    //{
    //    private readonly string _name = name;

    //    public override string ToWhereClause()
    //    {
    //        return "Name LIKE @Name";
    //    }
    //    public override object GetParameters()
    //    {
    //        return new { Name = $"%{_name}%" };
    //    }
    //    public string Name { get; set; } = string.Empty;
    //}

    //public class OpenRestaurantsSpec : BaseSpecification<Restaurant>
    //{
    //    public override string ToWhereClause()
    //    {
    //        return "HasDelivery = @HasDelivery";
    //    }

    //    public override object GetParameters()
    //    {
    //        return new { HasDelivery = true };
    //    }
    //}

}
