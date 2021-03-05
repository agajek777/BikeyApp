using System.Threading;
using System.Threading.Tasks;
using Application.Bikes.Commands;
using Application.Common.Interfaces;
using Domain.DTOs;
using LanguageExt.Common;
using MediatR;

namespace Application.Bikes.Handlers
{
    public class AddBikeHandler : IRequestHandler<AddBikeCommand, Result<BikeResponse>>
    {
        private readonly IBikeService _bikeService;

        public AddBikeHandler(IBikeService bikeService)
        {
            _bikeService = bikeService;
        }

        public async Task<Result<BikeResponse>> Handle(AddBikeCommand request, CancellationToken cancellationToken)
        {
            return await _bikeService.AddBikeAsync(request);
        }
    }
}