using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Extensions
{
   public static class QueryableExtensions
    {
        //public static IQueryable<T> Where<T>(this IQueryable<T> query, string whereClause, object parameters) where T : class
        //{
        //    // Access the DbContext to get the DbSet
        //    var dbContext = (query as IQueryable<Restaurant>).Provider.Context;
        //    var dbSet = dbContext.Set<T>();

        //    // Use FromSqlRaw with DbSet<T>
        //    return dbSet.FromSqlRaw($"SELECT * FROM {typeof(T).Name} WHERE {whereClause}", parameters);
        //}
    }
}
