using System;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Restaurants.Infrastructure.Jobs;

namespace Restaurants.Infrastructure.Extensions
{
    public static class QuartzServiceExtension // ❗️ Ye class missing thi
    {
        public static IServiceCollection AddQuartzJobs(this IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                var jobKey = new JobKey("DeactivateOldRestaurantsJob");

                q.AddJob<DeactivateOldRestaurantsJob>(opts => opts.WithIdentity(jobKey));
                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("DeactivateOldRestaurantsJob-trigger")
                    .WithSimpleSchedule(x => x
                        .WithIntervalInHours(24)
                        .RepeatForever()));
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
            return services;
        }
    }
}
