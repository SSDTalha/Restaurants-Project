using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantDto
{
    public RestaurantDto()
    {
    }

    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }

    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public List<DishDto>? Dishes { get; set; } = new();   //[]

    public static RestaurantDto? FromEntity(Restaurant? restaurant)
    {
        if (restaurant == null) return null;

        return new RestaurantDto
        {
            Id = restaurant.Id,
            Name = restaurant.Name ?? string.Empty,
            Description = restaurant.Description ?? string.Empty,
            Category = restaurant.Category ?? string.Empty,
            HasDelivery = restaurant.HasDelivery,
            City = restaurant.Address?.City, // No change needed
            Street = restaurant.Address?.Street,
            PostalCode = restaurant.Address?.PostalCode,
            Dishes = restaurant.Dishes?.Where(dish => dish != null) // Filter null dishes
            .Select(dish => DishDto.FromEntity(dish)) // Use non-nullable version
            .ToList() ?? new List<DishDto>()
        };
    }
}
