using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
        
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<HomeBase> HomeBases { get; set; }
        public DbSet<Model> Models { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bike>().Property(b => b.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<HomeBase>().Property(b => b.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Model>().Property(b => b.Id).ValueGeneratedOnAdd();
            
            modelBuilder.Entity<Bike>()
                .Property(b => b.State)
                .HasConversion<string>();
            
            modelBuilder.Entity<Bike>()
                .Property(b => b.Size)
                .HasConversion<string>();

            modelBuilder.Entity<Model>()
                .Property(m => m.Type)
                .HasConversion<string>();
            
            base.OnModelCreating(modelBuilder);
        }
    }
}