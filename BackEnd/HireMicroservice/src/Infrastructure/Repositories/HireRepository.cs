using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Enums;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Communication;
using Application.Hires.Commands;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Communication;
using Infrastructure.Persistence;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Error = Application.Common.Errors.Error;

namespace Infrastructure.Repositories
{
    public class HireRepository : IHireRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IEventPublisher _client;

        public HireRepository(ApplicationDbContext dbContext, IMapper mapper, IEventPublisher client)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _client = client;
        }

        public async Task<Result<List<HireResponse>>> GetAllHiresAsync()
        {
            var hires = await _dbContext.Hires
                .Include(h => h.Client)
                .Include(h => h.Bike)
                .Select(h => _mapper.Map<HireResponse>(h))
                .ToListAsync();

            return new Result<List<HireResponse>>(hires);
        }

        public async Task<Result<bool>> CheckIfExistsAsync(string requestId)
        {
            var result = await _dbContext.Hires.AnyAsync(h => h.Id == requestId);

            if (!result)
                return new Result<bool>(new BadRequestException(Error.HireNotFound));
            
            return new Result<bool>(true);
        }

        public async Task<Result<HireResponse>> GetHireAsync(string requestId)
        {
            var result = await CheckIfExistsAsync(requestId);

            if (result.IsFaulted)
                return new Result<HireResponse>(new InternalServerException(Error.ErrorWhileProcessing));

            var hireInDb = await _dbContext.Hires.FindAsync(requestId);

            return _mapper.Map<HireResponse>(hireInDb);
        }

        public async Task<Result<HireResponse>> CreateHireAsync(CreateHireCommand request)
        {
            var hireToAdd = _mapper.Map<Hire>(request);

            _dbContext.Hires.Add(hireToAdd);

            var bikeInDb = await _dbContext.Bikes.FindAsync(request.BikeId);

            SetBikeState(ref bikeInDb, State.Hired);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new Result<HireResponse>(new InternalServerException(Error.ErrorWhileProcessing));
            }

            return new Result<HireResponse>(_mapper.Map<HireResponse>(hireToAdd));
        }

        public async Task<Result<HireResponse>> UpdateHireAsync(UpdateHireCommand request)
        {
            var hireInDb = await _dbContext.Hires.FindAsync(request.Id);

            if (hireInDb is null)
                return new Result<HireResponse>(new InternalServerException(Error.ErrorWhileProcessing));

            if (hireInDb.State == HireState.Active && request.State == HireState.Terminated)
            {
                var bikeInDb = await _dbContext.Bikes.FindAsync(request.BikeId);

                SetBikeState(ref bikeInDb, State.Free);
            }

            hireInDb = _mapper.Map(request, hireInDb);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new Result<HireResponse>(new InternalServerException(Error.ErrorWhileProcessing));
            }

            return new Result<HireResponse>(_mapper.Map<HireResponse>(hireInDb));
        }

        public async Task<Result<bool>> DeleteHireAsync(string requestId)
        {
            var hireInDb = await _dbContext.Hires.FindAsync(requestId);

            if (hireInDb is null)
                return new Result<bool>(new InternalServerException(Error.ErrorWhileProcessing));

            _dbContext.Remove(hireInDb);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new Result<bool>(new InternalServerException(Error.ErrorWhileProcessing));
            }

            return new Result<bool>(true);
        }

        private void PublishBikePutEvent(Bike bikeInDb)
        {
            _client.PublishEvent(new HireEventMessage()
            {
                MessageType = bikeInDb.GetType().Name,
                Method = ApiMethod.PUT.ToString(),
                Message = bikeInDb
            });
        }
        
        private void SetBikeState(ref Bike bikeInDb, State state)
        {
            bikeInDb.State = state;

            PublishBikePutEvent(bikeInDb);

            _dbContext.Bikes.Update(bikeInDb);
        }
        
    }
}