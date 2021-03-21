using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Hires.Commands;
using Domain.Dtos;
using LanguageExt.Common;
using Error = Application.Common.Errors.Error;

namespace Infrastructure.Services
{
    public class HireService : IHireService
    {
        private readonly IHireRepository _hireRepository;
        private readonly IBikeService _bikeService;
        private readonly IClientService _clientService;

        public HireService(IHireRepository hireRepository, IBikeService bikeService, IClientService clientService)
        {
            _hireRepository = hireRepository;
            _bikeService = bikeService;
            _clientService = clientService;
        }

        public async Task<Result<List<HireResponse>>> GetAllHiresAsync() 
            => await _hireRepository.GetAllHiresAsync();

        public async Task<Result<bool>> CheckIfExistsAsync(string requestId)
            => await _hireRepository.CheckIfExistsAsync(requestId);

        public async Task<Result<HireResponse>> GetHireAsync(string requestId)
        {
            var checkResult = await CheckIfExistsAsync(requestId);

            if (checkResult.IsFaulted)
                return new Result<HireResponse>(new BadRequestException(Error.HireNotFound));

            return await _hireRepository.GetHireAsync(requestId);
        }

        public async Task<Result<HireResponse>> CreateHireAsync(CreateHireCommand request)
        {
            var validationResult = await BikeAndClientValidationAsync(request);

            if (validationResult.IsFaulted)
                return validationResult;

            return await _hireRepository.CreateHireAsync(request);
        }

        public async Task<Result<HireResponse>> UpdateHireAsync(UpdateHireCommand request)
        {
            var hireResult = await _hireRepository.GetHireAsync(request.Id);

            if (hireResult.IsFaulted)
                return hireResult;

            HireResponse hireResponse = new();

            hireResult.IfSucc(s => hireResponse = s);

            if (request.BikeId != hireResponse.BikeId
                || request.ClientId != hireResponse.ClientId)
                return new Result<HireResponse>(new BadRequestException(Error.CannotModifyHire));

            return await _hireRepository.UpdateHireAsync(request);
        }

        public async Task<Result<bool>> DeleteHireAsync(string requestId)
        {
            var checkResult = await CheckIfExistsAsync(requestId);

            if (checkResult.IsFaulted)
                return new Result<bool>(new BadRequestException(Error.HireNotFound));

            return await _hireRepository.DeleteHireAsync(requestId);
        }

        private async Task<Result<HireResponse>> BikeAndClientValidationAsync(CreateHireCommand request)
        {
            Exception exception = new ();

            var bikeCheck = await _bikeService.CheckIfBikeAvailableAsync(request.BikeId);
            if (bikeCheck.IsFaulted)
            {
                var result = bikeCheck.IfFail(f => exception = f);
                return new Result<HireResponse>(exception);
            }
            
            var clientCheck = await _clientService.CheckIfClientAvailableAsync(request.ClientId);
            if (clientCheck.IsFaulted)
            {
                var result = clientCheck.IfFail(f => exception = f);
                return new Result<HireResponse>(exception);
            }

            return new Result<HireResponse>();
        }
    }
}