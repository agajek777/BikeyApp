using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Services
{
    public class HomeBaseService : IHomeBaseService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public HomeBaseService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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

        public Task DeleteHomeBaseAsync(HomeBase homeBase)
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateHomeBaseAsync(HomeBase homeBase)
        {
            var homeBaseInDb = _dbContext.HomeBases.Find(homeBase.Id);

            homeBaseInDb = _mapper.Map(homeBase, homeBaseInDb);

            _dbContext.HomeBases.Update(homeBaseInDb);

            await _dbContext.SaveChangesAsync();
        }
    }
}