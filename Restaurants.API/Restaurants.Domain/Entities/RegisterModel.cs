using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Entities
{
    public class RegisterModel
    {
        public  required string Email { get; set; }
        public  required string Password { get; set; }
        public int CompanyId { get; set; }
    }

}
