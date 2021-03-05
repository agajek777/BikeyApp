using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<HomeBase> HomeBases { get; set; }
        public DbSet<Model> Models { get; set; }
    }
}