using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
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

        public BikeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
    }
}