using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Bikes.Commands;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Persistence;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Error = Application.Common.Errors.Error;

namespace Infrastructure.Repositories
{
    public class BikeRepository : IBikeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public BikeRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool CheckIfExists(string requestId)
        {
            return _dbContext.Bikes.Exists(b => b.Id == requestId);
        }

        public async Task<Result<BikeResponse>> GetBikeAsync(string requestId)
        {
            var bikeInDb = await _dbContext.Bikes.FindAsync(requestId);

            return (bikeInDb is null) ? 
                new Result<BikeResponse>(new BadRequestException(Error.BikeNotFound))
                    : new Result<BikeResponse>(_mapper.Map<BikeResponse>(bikeInDb));
        }

        public async Task<Result<List<BikeResponse>>> GetAllBikesAsync()
        {
            return new Result<List<BikeResponse>>(
                await _dbContext.Bikes
                    .Select(b => _mapper.Map<BikeResponse>(b))
                    .ToListAsync());
        }

        public async Task<Result<BikeResponse>> AddBikeAsync(AddBikeCommand request)
        {
            var bikeToAdd = _mapper.Map<Bike>(request);

            _dbContext.Bikes.Add(bikeToAdd);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new Result<BikeResponse>(new InternalErrorException(Error.ErrorWhileProcessingOperation));
            }

            return new Result<BikeResponse>(_mapper.Map<BikeResponse>(bikeToAdd));
        }

        public async Task<Result<BikeResponse>> UpdateBikeAsync(UpdateBikeCommand request)
        {
            var bikeInDb = await _dbContext.Bikes.FindAsync(request.Id);

            if (bikeInDb is null)
                return new Result<BikeResponse>(new InternalErrorException(Error.BikeNotFound));

            bikeInDb = _mapper.Map(request, bikeInDb);

            _dbContext.Update(bikeInDb);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new Result<BikeResponse>(new InternalErrorException(Error.ErrorWhileProcessingOperation));
            }

            return new Result<BikeResponse>(_mapper.Map<BikeResponse>(bikeInDb));
        }

        public async Task<Result<bool>> DeleteBikeAsync(string requestId)
        {
            var bikeInDb = await _dbContext.Bikes.FindAsync(requestId);

            if (bikeInDb is null)
                return new Result<bool>(new InternalErrorException(Error.BikeNotFound));

            _dbContext.Bikes.Remove(bikeInDb);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new Result<bool>(new InternalErrorException(Error.ErrorWhileProcessingOperation));
            }

            return new Result<bool>(true);
        }
    }
}