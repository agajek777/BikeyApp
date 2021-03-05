using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Bikes.Queries;
using Application.Common.Interfaces;
using Domain.DTOs;
using LanguageExt.Common;
using MediatR;

namespace Application.Bikes.Handlers
{
    public class GetAllBikesHandler : IRequestHandler<GetAllBikesQuery, Result<List<BikeResponse>>>
    {
        private readonly IBikeService _bikeService;

        public GetAllBikesHandler(IBikeService bikeService)
        {
            _bikeService = bikeService;
        }

        public async Task<Result<List<BikeResponse>>> Handle(GetAllBikesQuery request, CancellationToken cancellationToken)
        {
            return await _bikeService.GetAllBikesAsync();
        }
    }
}