using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.HomeBases.Commands;
using Domain.DTOs;
using LanguageExt.Common;
using Error = Application.Common.Errors.Error;

namespace Infrastructure.Services
{
    public class HomeBaseService : IHomeBaseService
    {
        private readonly IHomeBaseRepository _homeBaseRepository;

        public HomeBaseService(IHomeBaseRepository homeBaseRepository)
        {
            _homeBaseRepository = homeBaseRepository;
        }

        public async Task<bool> CheckIfUniqueAsync(string name)
        {
            return await _homeBaseRepository.CheckIfUniqueAsync(name);
        }

        public async Task<Result<HomeBaseResponse>> AddHomeBaseAsync(AddHomeBaseCommand request)
        {
            if (!await CheckIfUniqueAsync(request.Name))
                return new Result<HomeBaseResponse>(new BadRequestException(Error.InvalidHomeBaseName));

            return await _homeBaseRepository.AddHomeBaseAsync(request);
        }

        public async Task<Result<HomeBaseResponse>> GetHomeBaseAsync(string requestId)
        {
            if (!await CheckIfExistsAsync(requestId))
                return new Result<HomeBaseResponse>(new BadRequestException(Error.HomeBaseNotFound));

            return await _homeBaseRepository.GetHomeBaseAsync(requestId);
        }

        public async Task<Result<List<HomeBaseResponse>>> GetAllHomeBasesAsync()
        {
            return await _homeBaseRepository.GetAllHomeBasesAsync();
        }

        public async Task<bool> CheckIfExistsAsync(string requestId)
        {
            return await _homeBaseRepository.CheckIfExists(requestId);
        }

        public async Task<Result<HomeBaseResponse>> UpdateHomeBaseAsync(UpdateHomeBaseCommand request)
        {
            var homeBaseResponse = new HomeBaseResponse();
            
            var result = (await GetHomeBaseAsync(request.Id))
                .Match(s => homeBaseResponse = s,
                    f => new Result<HomeBaseResponse>(f));

            if (result.IsFaulted)
                return result;

            if (homeBaseResponse.Name != request.Name)
                if (!await CheckIfUniqueAsync(request.Name))
                    return new Result<HomeBaseResponse>(new BadRequestException(Error.InvalidHomeBaseName));

            return await _homeBaseRepository.UpdateHomeBaseAsync(request);
        }

        public async Task<Result<bool>> DeleteHomeBaseAsync(string requestId)
        {
            if (!await CheckIfExistsAsync(requestId))
                return new Result<bool>(new BadRequestException(Error.HomeBaseNotFound));

            return await _homeBaseRepository.DeleteHomeBaseAsync(requestId);
        }
    }
}