using Microsoft.Extensions.Configuration;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders;

internal class RestaurantSeeder(RestaurantsDbContext dbContext, IConfiguration configuration) : IRestaurantSeeder
{
    public async Task Seed()
    {
        //if (await dbContext.Database.CanConnectAsync())
        //{
        var connStr = configuration.GetConnectionString("DefaultConnection");
        try
        {
            // check connection

            if (!dbContext.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                dbContext.Restaurants.AddRange(restaurants);
                await dbContext.SaveChangesAsync();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("Seeding failed: " + ex.Message);
            throw;
        }

        //}
    }

    private List<Restaurant> GetRestaurants()
    {
        List<Restaurant> restaurants = new List<Restaurant>
    {
        new()
        {
            Name = "Food Fusion",
            Description = "Best fusion cuisine in town",
            Category = "Fusion",
            HasDelivery = true,
            ContactEmail = "info@foodfusion.com",
            ContactNumber = "0300-1234567",
            Address = new Address
            {
                Street = "123 Main Street",
                City = "Lahore",
            },
            Dishes = new List<Dish>
            {
                new Dish
                {
                    Name = "Chicken Tikka Pizza",
                    Description = "Spicy chicken tikka on a cheesy base",
                    Price = 899.99m
                },
                new Dish
                {
                    Name = "Beef Lasagna",
                    Description = "Classic Italian-style beef lasagna",
                    Price = 999.99m
                }
            }
        },
        new Restaurant
        {
            Name = "Desi Delight",
            Description = "Traditional Pakistani cuisine",
            Category = "Desi",
            HasDelivery = false,
            ContactEmail = "contact@desidelight.pk",
            ContactNumber = "0311-9876543",
            Address = new Address
            {
                Street = "45 Food Street",
                City = "Karachi",
            },
            Dishes = new List<Dish>
            {
                new Dish
                {
                    Name = "Nihari",
                    Description = "Slow-cooked beef stew",
                    Price = 499.00m
                },
                new Dish
                {
                    Name = "Biryani",
                    Description = "Spicy chicken biryani with raita",
                    Price = 599.00m
                }
            }
        },
        new Restaurant
        {
            Name = "Burger Hub",
            Description = "Juicy burgers and crispy fries",
            Category = "Fast Food",
            HasDelivery = true,
            ContactEmail = "orders@burgerhub.com",
            ContactNumber = "0322-4455667",
            Address = new Address
            {
                Street = "78 Grill Avenue",
                City = "Islamabad",
            },
            Dishes = new List<Dish>
            {
                new Dish
                {
                    Name = "Double Cheese Burger",
                    Description = "Loaded with double cheese and beef patty",
                    Price = 749.00m
                },
                new Dish
                {
                    Name = "Zinger Fries",
                    Description = "Crispy fries topped with spicy zinger bits",
                    Price = 299.00m
                }
            }
        },
        new Restaurant
        {
            Name = "Green Bowl",
            Description = "Healthy and fresh salad options",
            Category = "Healthy",
            HasDelivery = false,
            ContactEmail = "hello@greenbowl.pk",
            ContactNumber = "0345-6677889",
            Address = new Address
            {
                Street = "12 Wellness Street",
                City = "Faisalabad",
            },
            Dishes = new List<Dish>
            {
                new Dish
                {
                    Name = "Caesar Salad",
                    Description = "Fresh romaine with grilled chicken and dressing",
                    Price = 549.00m
                },
                new Dish
                {
                    Name = "Quinoa Bowl",
                    Description = "A mix of quinoa, avocado, and greens",
                    Price = 649.00m
                }
            }
        },
        new Restaurant
        {
            Name = "Sweet Tooth",
            Description = "Delicious desserts and sweet treats",
            Category = "Desserts",
            HasDelivery = true,
            ContactEmail = "info@sweettooth.com",
            ContactNumber = "0309-9988776",
            Address = new Address
            {
                Street = "101 Dessert Lane",
                City = "Rawalpindi",
            },
            Dishes = new List<Dish>
            {
                new Dish
                {
                    Name = "Chocolate Lava Cake",
                    Description = "Molten chocolate cake served warm",
                    Price = 399.00m
                },
                new Dish
                {
                    Name = "Strawberry Cheesecake",
                    Description = "Classic cheesecake with fresh strawberries",
                    Price = 449.00m
                }
            }
        }
    };

        return restaurants;
    }
}
