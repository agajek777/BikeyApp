using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IDbContext
    {
        public DbSet<Bike> Bikes { get; set; }
        
        public DbSet<Client> Clients { get; set; }

        public DbSet<Hire> Hires { get; set; }

        public DbSet<HomeBase> HomeBases { get; set; }
    }
}