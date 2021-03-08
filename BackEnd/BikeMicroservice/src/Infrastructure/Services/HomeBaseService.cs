using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Services
{
    public class HomeBaseService : IHomeBaseService
    {
        private readonly ApplicationDbContext _dbContext;

        public HomeBaseService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CheckIfExistsAsync(string requestHomeBaseId)
        {
            return _dbContext.HomeBases.Exists(b => b.Id == requestHomeBaseId);
        }

        public async Task<bool> CheckIfFreeSlotsAsync(string requestHomeBaseId)
        {
            var homeBase = await _dbContext.HomeBases.FindAsync(requestHomeBaseId);

            var bikesCount = _dbContext.Bikes.Where(b => b.HomeBaseId == requestHomeBaseId).Count();

            return (bikesCount < homeBase.Capacity);
        }

        public async Task AddHomeBaseAsync(HomeBase homeBase)
        {
            _dbContext.Add(homeBase);
            
            await _dbContext.SaveChangesAsync();
        }
    }
}