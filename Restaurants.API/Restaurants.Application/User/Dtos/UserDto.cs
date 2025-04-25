namespace Restaurants.Application.User.Dtos
{
    public class UserDto
    {
        public string Username { get; set; }
        public required string Token { get; set; }
    }
}
