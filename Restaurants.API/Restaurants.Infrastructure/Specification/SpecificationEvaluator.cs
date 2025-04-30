using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Domain.Specification;
namespace Restaurants.Infrastructure.Specification
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery, ISpecificationo<T> specification)
            where T : class
        {
            return inputQuery.Where(specification.Criteria);
        }
    }
}
