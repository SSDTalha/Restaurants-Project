using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Specification;
namespace Restaurants.API.Controllers;


[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(IMediator mediator, IGenericRepository<Restaurant> restaurantRepo) : ControllerBase


{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
        return Ok(restaurants);
    }


    [HttpGet("dapper")]
    public async Task<IActionResult> GetAllWithDapper(
       [FromServices] IDapperRepository dapperRepository)
    {
        // Explicitly request Dapper implementation
        var restaurants = await dapperRepository.GetAllAsync();
        return Ok(restaurants);
    }

    private readonly IGenericRepository<Restaurant> _restaurantRepo = restaurantRepo;
    [HttpGet("search")]
    public async Task<IActionResult> SearchRestaurants(string name)
    {
        var spec = new RestaurantByNameSpec(name);
        var restaurants = await _restaurantRepo.QueryAsync(spec);
        return Ok(restaurants);
    }

    [HttpGet("open")]
    public async Task<IActionResult> GetOpenRestaurants()
    {
        var spec = new OpenRestaurantsSpec();
        var restaurants = await _restaurantRepo.QueryAsync(spec);
        return Ok(restaurants);
    }
    //[HttpGet("check-company")]
    //public IActionResult CheckCompany()
    //{
    //    return Ok(new { currentUser.CompanyId });
    //}



    [HttpGet("{id}")]
    public async Task<ActionResult<RestaurantDto?>> GetById([FromRoute] int id)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "id claim type")!.Value;
        var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id, userId));

        if (restaurant == null)
        {
            return NotFound();
        }
        return Ok(restaurant);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
    {
        var isDeleted = await mediator.Send(new DeleteRestaurantCommand(id));

        if (isDeleted)
        {
            return NoContent();
        }
        return NotFound();
    }
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, UpdateRestaurantCommand command)
    {
        command.Id = id;
        var isUpdated = await mediator.Send(command);

        if (isUpdated)
        {
            return NoContent();
        }
        return NotFound();
    }


    [HttpPost]
    public async Task<IActionResult> CreateRestaurants(CreateRestaurantCommand command)
    {
        //if (!ModelState.IsValid)
        //{
        //    return BadRequest(ModelState);
        //}
        int id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }
}