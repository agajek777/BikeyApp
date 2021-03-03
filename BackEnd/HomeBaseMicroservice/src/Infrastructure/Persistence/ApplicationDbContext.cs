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
            modelBuilder.Entity<HomeBase>().Property(h => h.Id).ValueGeneratedOnAdd();
            
            base.OnModelCreating(modelBuilder);
        }
    }
}