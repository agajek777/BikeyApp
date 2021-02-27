using System.Linq.Expressions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<HomeBase> HomeBases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HomeBase>().OwnsOne(typeof(Capacity), "Capacity");
            modelBuilder.Entity<HomeBase>().OwnsOne(typeof(CoordinateLon), "CoordinateLon");
            modelBuilder.Entity<HomeBase>().OwnsOne(typeof(CoordinateLat), "CoordinateLat");
            
            base.OnModelCreating(modelBuilder);
        }
    }
}