using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
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
                where homebase.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)
                select homebase;

            return (await result.AnyAsync());
        }

        public async Task<Result<HomeBaseResponse>> AddHomeBaseAsync(AddHomeBaseCommand request)
        {
            var baseToAdd = _mapper.Map<HomeBase>(request);

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

        public Task<Result<HomeBaseResponse>> GetHomeBaseAsync(string requestId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<List<HomeBaseResponse>>> GetAllHomeBasesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CheckIfExists(string requestId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<HomeBaseResponse>> UpdateHomeBaseAsync(UpdateHomeBaseCommand request)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<bool>> DeleteHomeBaseAsync(string requestId)
        {
            throw new System.NotImplementedException();
        }
    }
}