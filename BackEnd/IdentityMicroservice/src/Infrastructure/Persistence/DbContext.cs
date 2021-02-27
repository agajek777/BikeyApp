using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class DbContext : IdentityDbContext<User, Role, string>, IDbContext
    {
        public DbContext(DbContextOptions options) : base(options)
        {
        }
    }
}