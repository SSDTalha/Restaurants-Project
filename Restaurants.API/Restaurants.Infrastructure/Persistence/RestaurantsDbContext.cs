﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        // OnModelCreating method to add global filter
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Restaurant>()
                .OwnsOne(r => r.Address);

            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.Dishes)
                .WithOne()
                .HasForeignKey(d => d.RestaurantId);

            // Apply Global Filter for CompanyId to Restaurant entity
            modelBuilder.Entity<Restaurant>()
                .HasQueryFilter(r => r.CompanyId == _currentUser.CompanyId);

            Console.WriteLine($"CompanyId in ICurrentUserService: {_currentUser.CompanyId}");


        }

        // SaveChangesAsync to set auditing fields
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<AuditableTenantEntity>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.CreatedBy = _currentUser.UserName ?? "System";
                    entry.Entity.CompanyId = _currentUser.CompanyId ?? string.Empty;
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
