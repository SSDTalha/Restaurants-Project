using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Specification;

namespace Restaurants.Application.Specification
{
    public class RestaurantByIdSpecification : BaseSpecification<Restaurant>
    {
        public RestaurantByIdSpecification(int id): base(r => r.Id == id)
        {
        }

        public override object[] GetParameters()
        {
            return new object[] { Id };
        }

        public override string ToWhereClause()
        {
            return "Id = @Id"; // Simple condition for fetching restaurant by Id
        }
    }
}
