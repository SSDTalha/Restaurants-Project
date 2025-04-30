using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Specification
{
    public class GetRestaurantByIdSpecification : BaseSpecification<Restaurant>
    {
        private readonly int _id;

        public GetRestaurantByIdSpecification(int id)
            : base(r => r.Id == id)   
        {
            _id = id;
        }

        public override object[] GetParameters()
        {
            return new object[] { _id };
        }

        public override string ToWhereClause()
        {
            return $"Id = {_id}";
        }
    }


}
