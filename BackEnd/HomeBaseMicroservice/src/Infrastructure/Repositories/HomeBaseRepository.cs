using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.HomeBases.Commands;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Persistence;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Error = Application.Common.Errors.Error;

namespace Infrastructure.Repositories
{
    public class HomeBaseRepository : IHomeBaseRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public HomeBaseRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> CheckIfUniqueAsync(string name)
        {
            var result = 
                from homebase in _dbContext.HomeBases
                where EF.Functions.Like(homebase.Name, name)
                select homebase;

            return (!await result.AnyAsync());
        }

        public async Task<Result<HomeBaseResponse>> AddHomeBaseAsync(AddHomeBaseCommand request)
        {
            HomeBase baseToAdd = new HomeBase();
            
            try
            {
                baseToAdd = _mapper.Map<HomeBase>(request);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new Result<HomeBaseResponse>(e);
            }

            _dbContext.Add(baseToAdd);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new Result<HomeBaseResponse>(new InternalErrorException(Error.ErrorWhileProcessingOperation));
            }

            return new Result<HomeBaseResponse>(_mapper.Map<HomeBaseResponse>(baseToAdd));

        }

        public async Task<Result<HomeBaseResponse>> GetHomeBaseAsync(string requestId)
        {
            var baseInbDb = await _dbContext.HomeBases.FindAsync(requestId);

            if (baseInbDb is null)
                return new Result<HomeBaseResponse>(
                    new InternalErrorException(Error.ErrorWhileProcessingOperation));

            var baseResponse = _mapper.Map<HomeBaseResponse>(baseInbDb);
            
            return new Result<HomeBaseResponse>(baseResponse);
        }

        public async Task<Result<List<HomeBaseResponse>>> GetAllHomeBasesAsync()
        {
            return await _dbContext.HomeBases
                .Select(b => _mapper.Map<HomeBaseResponse>(b))
                .ToListAsync();
        }

        public async Task<bool> CheckIfExists(string requestId)
        {
            var result = await _dbContext.HomeBases.FindAsync(requestId);

            return (result is not null);
        }

        public async Task<Result<HomeBaseResponse>> UpdateHomeBaseAsync(UpdateHomeBaseCommand request)
        {
            var baseToUpdate = await _dbContext.HomeBases.FindAsync(request.Id);

            var copy = baseToUpdate;

            baseToUpdate = _mapper.Map<UpdateHomeBaseCommand, HomeBase>(request, baseToUpdate);

            _dbContext.Update(baseToUpdate);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new Result<HomeBaseResponse>(new InternalErrorException(Error.ErrorWhileProcessingOperation));
            }

            return new Result<HomeBaseResponse>(_mapper.Map<HomeBaseResponse>(baseToUpdate));
        }

        public async Task<Result<bool>> DeleteHomeBaseAsync(string requestId)
        {
            var baseInDb = await _dbContext.HomeBases.FindAsync(requestId);

            if (baseInDb is null)
                return new Result<bool>(new InternalErrorException(Error.ErrorWhileProcessingOperation));

            _dbContext.HomeBases.Remove(baseInDb);

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