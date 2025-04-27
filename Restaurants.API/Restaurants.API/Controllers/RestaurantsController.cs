using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
public class RestaurantsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IGenericRepository<Restaurant> _restaurantRepo;
    //private readonly IConfiguration _config;
    //private readonly IdentityUser _user;
    //private readonly JwtService _jwtService;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    public RestaurantsController(IMediator mediator, IGenericRepository<Restaurant> restaurantRepo, /*IConfiguration config ,IdentityUser user, JwtService jwtService, */UserManager<User> userManager, IConfiguration configuration /*, ITokenService tokenService */)
    {
        _mediator = mediator;
        _restaurantRepo = restaurantRepo;
        //_config = config;
        //_user = user;
        //_jwtService = jwtService;
        //_tokenService = tokenService;
        _userManager = userManager;
        _configuration = configuration;

    }



    [HttpGet]
    [Authorize]
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


    //[HttpPost("register")] //aacount register

    //public async Task<ActionResult<(User)>>
    // {
    //      using var hmac = new HMACSHA512();

    //var user = new User
    //{
    //    //   PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(identityregister),
    //    PasswordSalt = hmac.Key,
    //};

    //context.User.Add(user);
    [HttpPost("login")]
    public IActionResult Login(LoginModelJwt model)
    {
        // Normally yahan database se user verify karte hain
        // Filhal hardcode assume karte hain ke user valid hai

        if (model.Email == "talhaimtiaz023@gmail.com" && model.Password == "Snoopd@g125")
        {
            // Token Generate karo
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("YehKoiStrongSecretKeyHai123!YehKoiStrongSecretKeyHai123!YehKoiStrongSecretKeyHai123!YehKoiStrongSecretKeyHai123!"); // <-- Apni key

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, model.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = "yourdomain.com",
                Audience = "yourdomain.com",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            // Return JWT token
            return Ok(new { token = jwt });
        }

        return Unauthorized();
    }

}





