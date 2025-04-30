using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Jobs
{
    public class DeactivateOldRestaurantsJob(RestaurantsDbContext context) : IJob
    {


        private readonly RestaurantsDbContext _context = context;

        public async Task Execute(IJobExecutionContext jobContext)
        {
          var thresholdDate = DateTime.UtcNow.AddDays(-30);
           var outDatedRestaurants = await _context.Restaurants
                .Where(r => r.CreatedAt < thresholdDate)
                .ToListAsync();
       
            
            foreach (var restaurant in outDatedRestaurants)
            {
                restaurant.IsActive = false;
            }
           if(outDatedRestaurants.Any())
            {
                await _context.SaveChangesAsync();
                Console.WriteLine($"✅ {outDatedRestaurants.Count} restaurants deactivated at {DateTime.Now}");
            }
            else
            {
                Console.WriteLine($"ℹ️ No outdated restaurants found at {DateTime.Now}");
            }
        }
    }
   
}
