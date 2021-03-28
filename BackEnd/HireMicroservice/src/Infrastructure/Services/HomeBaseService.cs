using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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

        public async Task AddHomeBaseAsync(HomeBase homeBase)
        {
            _dbContext.HomeBases.Add(homeBase);
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteHomeBaseAsync(HomeBase homeBase)
        {
            var homeBaseInDb = _dbContext.HomeBases.Find(homeBase.Id);
            
            _dbContext.HomeBases.Remove(homeBaseInDb);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateHomeBaseAsync(HomeBaseUpdateDto homeBase)
        {
            var homeBaseInDb = _dbContext.HomeBases.Find(homeBase.Id);

            homeBaseInDb = _mapper.Map(homeBase, homeBaseInDb);
            
            _dbContext.HomeBases.Update(homeBaseInDb);

            await _dbContext.SaveChangesAsync();
        }
    }
}