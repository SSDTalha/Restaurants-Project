using Microsoft.AspNetCore.Identity;

namespace Restaurants.Domain.Entities;

public class User : IdentityUser
{
    public long CompanyId { get; set; }
    //public int id { get; set; }

    //public required string UserName { get; set; }
    //public byte[] PasswordHash { get; set; }
    //public required byte[] PasswordSalt { get; set; }

}

