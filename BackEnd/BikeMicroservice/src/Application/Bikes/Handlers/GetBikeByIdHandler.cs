using System.Threading;
using System.Threading.Tasks;
using Application.Bikes.Queries;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.DTOs;
using LanguageExt.Common;
using MediatR;
using Error = Application.Common.Errors.Error;

namespace Application.Bikes.Handlers
{
    public class GetBikeByIdHandler : IRequestHandler<GetBikeById, Result<BikeResponse>>
    {
        private readonly IBikeService _bikeService;

        public GetBikeByIdHandler(IBikeService bikeService)
        {
            _bikeService = bikeService;
        }

        public async Task<Result<BikeResponse>> Handle(GetBikeById request, CancellationToken cancellationToken)
        {
            return await _bikeService.GetBikeAsync(request.Id);
        }
    }
}