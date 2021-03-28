using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Error = Application.Common.Errors.Error;

namespace Infrastructure.Services
{
    public class BikeService : IBikeService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public BikeService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<bool>> CheckIfBikeAvailableAsync(string requestBikeId)
        {
            var existCheck = await CheckIfBikeExistsAsync(requestBikeId);

            if (existCheck.IsFaulted)
                return existCheck;

            var bikeInDb = await _dbContext.Bikes.FindAsync(requestBikeId);

            if (bikeInDb.State != State.Free)
                return new Result<bool>(new BadRequestException(Error.BikeNotAvailable));

            return new Result<bool>(true);
        }

        public async Task<Result<bool>> CheckIfBikeExistsAsync(string requestBikeId)
        {
            var outcome = await _dbContext.Bikes.AnyAsync(b => b.Id == requestBikeId);

            if (!outcome)
                return new Result<bool>(new BadRequestException(Error.BikeNotExists));

            return new Result<bool>(true);
        }

        public async Task AddBikeAsync(Bike bike)
        {
            _dbContext.Add(bike);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBikeAsync(Bike bike)
        {
            _dbContext.Remove(bike);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateBikeAsync(Bike bike)
        {
            var bikeInDb = await _dbContext.Bikes.FindAsync(bike.Id);

            bikeInDb = _mapper.Map<Bike, Bike>(bike, bikeInDb);

            _dbContext.Update(bikeInDb);

            await _dbContext.SaveChangesAsync();
        }
    }
}