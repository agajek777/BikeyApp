using System.Threading;
using System.Threading.Tasks;
using Application.Bikes.Commands;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.DTOs;
using LanguageExt.Common;
using MediatR;
using Error = Application.Common.Errors.Error;

namespace Application.Bikes.Handlers
{
    public class UpdateBikeHandler : IRequestHandler<UpdateBikeCommand, Result<BikeResponse>>
    {
        private readonly IBikeService _bikeService;

        public UpdateBikeHandler(IBikeService bikeService)
        {
            _bikeService = bikeService;
        }

        public async Task<Result<BikeResponse>> Handle(UpdateBikeCommand request, CancellationToken cancellationToken)
        {
            return await _bikeService.UpdateBikeAsync(request);
        }
    }
}