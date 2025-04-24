
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Application.Interfaces;
using Restaurants.Domain.Entities;
namespace Restaurants.Infrastructure.Persistence
{
    public class RestaurantsDbContext : IdentityDbContext<User>
    {
        private readonly ICurrentUserService _currentUser;

        public RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options, ICurrentUserService currentUser)
            : base(options)
        {
            _currentUser = currentUser;
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Restaurant>()
                .OwnsOne(r => r.Address);

            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.Dishes)
                .WithOne()
                .HasForeignKey(d => d.RestaurantId);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(AuditableTenantEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(
               Expression.Convert(parameter, typeof(AuditableTenantEntity)), nameof(AuditableTenantEntity.CompanyId));
                    var companyIdValue = Expression.Constant((long)(_currentUser.CompanyId ?? 0), typeof(long));

                    var body = Expression.Equal(property, companyIdValue);
                    var lambda = Expression.Lambda(body, parameter);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                    Console.WriteLine($"CompanyId in ICurrentUserService: {_currentUser.CompanyId}");
                }
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var entries = ChangeTracker.Entries<AuditableTenantEntity>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.CreatedBy = _currentUser.UserName ?? "System";
                    entry.Entity.CompanyId = _currentUser.CompanyId ?? 0;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifiedAt = DateTime.Now;
                    entry.Entity.ModifiedBy = _currentUser.UserName ?? "System";
                }
            }


            return await base.SaveChangesAsync(cancellationToken);
        }
    }

}

