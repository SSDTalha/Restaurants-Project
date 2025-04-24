using MediatR;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQuery : IRequest<RestaurantDto?>
    {
        public int Id { get; }
        public string UserId { get; set; }

        public GetRestaurantByIdQuery(int id, string userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}