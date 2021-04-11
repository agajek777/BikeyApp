using System.Collections.Generic;
using Domain.DTOs;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace Application.Bikes.Queries
{
    public class GetBikesByHomeBaseId : IRequest<Result<List<BikeResponse>>>
    {
        public string HomeBaseId { get; set; }

        public GetBikesByHomeBaseId(string homeBaseId)
        {
            HomeBaseId = homeBaseId;
        }
    }
}