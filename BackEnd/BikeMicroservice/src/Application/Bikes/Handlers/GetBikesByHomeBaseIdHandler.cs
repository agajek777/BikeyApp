using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Bikes.Queries;
using Domain.DTOs;
using LanguageExt.Common;
using MediatR;

namespace Application.Bikes.Handlers
{
    public class GetBikesByHomeBaseIdHandler : IRequestHandler<GetAllBikesQuery, Result<List<BikeResponse>>>
    {
        public Task<Result<List<BikeResponse>>> Handle(GetAllBikesQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}