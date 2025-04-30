using AutoMapper;
using MediatR;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Specification;
namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQueryHandler : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto?>
    {
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IMapper _mapper;

        public GetRestaurantByIdQueryHandler(IRestaurantsRepository restaurantsRepository, IMapper mapper)
        {
            _restaurantsRepository = restaurantsRepository;
            _mapper = mapper;
        }

        public async Task<RestaurantDto?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            // Specification ka instance banayein
            var specification = new GetRestaurantByIdSpecification(request.Id);

            // Repository ko specification pass karke restaurant fetch karein
            var restaurant = await _restaurantsRepository.GetBySpecificationAsync(specification);

            // Mapping
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

            return restaurantDto;    //ye pehley comment kiya tha
        }
    }

}
