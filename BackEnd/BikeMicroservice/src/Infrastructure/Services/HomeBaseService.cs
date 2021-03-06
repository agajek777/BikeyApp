using System.Threading.Tasks;
using Application.Common.Interfaces;
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

            return (homeBase.Bikes.Count < homeBase.Capacity);
        }
    }
}