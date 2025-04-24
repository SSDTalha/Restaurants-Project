using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Identity
{
    public class LoginResponseModel
    {
        public string? Username { get; set; }

        public string? AcessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
