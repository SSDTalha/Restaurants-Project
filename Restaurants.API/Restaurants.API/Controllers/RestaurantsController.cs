using System.Security.Cryptography;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Interfaces;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Specification;
using Restaurants.Infrastructure.Helpers;
namespace Restaurants.API.Controllers;




[ApiController]
[Route("api/restaurants")]
public class RestaurantsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IGenericRepository<Restaurant> _restaurantRepo;
    private readonly IConfiguration _config;
    private readonly IdentityUser _user;
    private readonly JwtService _jwtService;

    public RestaurantsController(IMediator mediator, IGenericRepository<Restaurant> restaurantRepo, IConfiguration config, IdentityUser user, JwtService jwtService, ITokenService tokenService)
    {
        _mediator = mediator;
        _restaurantRepo = restaurantRepo;
        _config = config;
        _user = user;
        _jwtService = jwtService;
        _tokenService = tokenService;
    }



    [HttpGet]
    //[Authorize]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await _mediator.Send(new GetAllRestaurantsQuery());
        return Ok(restaurants);
    }

    [HttpGet("dapper")]
    public async Task<IActionResult> GetAllWithDapper(
       [FromServices] IDapperRepository dapperRepository)
    {
        var restaurants = await dapperRepository.GetAllAsync();
        return Ok(restaurants);
    }

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

    [HttpGet("{id}")]
    public async Task<ActionResult<RestaurantDto?>> GetById([FromRoute] int id)
    {
        var restaurant = await _mediator.Send(new GetRestaurantByIdQuery(id));

        if (restaurant == null)
        {
            return NotFound();
        }
        return Ok(restaurant);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
    {
        var isDeleted = await _mediator.Send(new DeleteRestaurantCommand(id));

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
        var isUpdated = await _mediator.Send(command);

        if (isUpdated)
        {
            return NoContent();
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurants(CreateRestaurantCommand command)
    {
        int id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }


    [HttpPost("register")] //aacount register

    public async Task<ActionResult<(User)>>
     {
          using var hmac = new HMACSHA512();

    var user = new User
    {
        //   PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(identityregister),
        PasswordSalt = hmac.Key,
    };

    context.User.Add(user);

}





