using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Bikes.Commands;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using LanguageExt.Common;
using Error = Application.Common.Errors.Error;

namespace Infrastructure.Services
{
    public class BikeService : IBikeService
    {
        private readonly IBikeRepository _bikeRepository;
        private readonly IHomeBaseService _homeBaseService;

        public BikeService(IBikeRepository bikeRepository, IHomeBaseService homeBaseService)
        {
            _bikeRepository = bikeRepository;
            _homeBaseService = homeBaseService;
        }

        public bool CheckIfExists(string requestId)
        {
            return _bikeRepository.CheckIfExists(requestId);
        }

        public async Task<Result<BikeResponse>> GetBikeAsync(string requestId)
        {
            if (!CheckIfExists(requestId))
                return new Result<BikeResponse>(new BadRequestException(Error.BikeNotFound));

            return await _bikeRepository.GetBikeAsync(requestId);
        }

        public async Task<Result<List<BikeResponse>>> GetAllBikesAsync()
        {
            return await _bikeRepository.GetAllBikesAsync();
        }

        public async Task<Result<BikeResponse>> AddBikeAsync(AddBikeCommand request)
        {
            // validation if bike is being added to the specific HomeBase ...
            if (request.HomeBaseId is not null)
            {
                if (!_homeBaseService.CheckIfExistsAsync(request.HomeBaseId))
                    return new Result<BikeResponse>(new BadRequestException(Error.HomeBaseNotFound));

                if (!await _homeBaseService.CheckIfFreeSlotsAsync(request.HomeBaseId))
                    return new Result<BikeResponse>(new BadRequestException(Error.HomeBaseFull));
            }

            return await _bikeRepository.AddBikeAsync(request);
        }

        public async Task<Result<BikeResponse>> UpdateBikeAsync(UpdateBikeCommand request)
        {
            var bikeResult = await _bikeRepository.GetBikeAsync(request.Id);
            BikeResponse bikeInDb = new BikeResponse();

            bikeResult.Match(s => bikeInDb = s,
                f => new Result<BikeResponse>(f));

            if (bikeResult.IsFaulted)
                return bikeResult;
            
            // validation if bike is being added to the specific HomeBase ...
            if (request.HomeBaseId is not null && bikeInDb.HomeBaseId != request.HomeBaseId)
            {
                if (!_homeBaseService.CheckIfExistsAsync(request.HomeBaseId))
                    return new Result<BikeResponse>(new BadRequestException(Error.HomeBaseNotFound));

                if (!await _homeBaseService.CheckIfFreeSlotsAsync(request.HomeBaseId))
                    return new Result<BikeResponse>(new BadRequestException(Error.HomeBaseFull));
            }

            return await _bikeRepository.UpdateBikeAsync(request);
        }

        public async Task<Result<bool>> DeleteBikeAsync(string requestId)
        {
            if (!CheckIfExists(requestId))
                return new Result<bool>(new BadRequestException(Error.BikeNotFound));

            return await _bikeRepository.DeleteBikeAsync(requestId);

        }
    }
}