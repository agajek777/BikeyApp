using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IDbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
        
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Hire> Hires { get; set; }
        public DbSet<HomeBase> HomeBases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hire>().Property(h => h.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Hire>()
                .Property(h => h.State)
                .HasConversion<string>();

            modelBuilder.Entity<Bike>()
                .Property(b => b.State)
                .HasConversion<string>();
            
            base.OnModelCreating(modelBuilder);
        }
    }
}