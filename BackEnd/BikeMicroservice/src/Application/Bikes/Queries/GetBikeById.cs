using Domain.DTOs;
using LanguageExt.Common;
using MediatR;

namespace Application.Bikes.Queries
{
    public class GetBikeById : IRequest<Result<BikeResponse>>
    {
        public string Id { get; set; }

        public GetBikeById(string id)
        {
            Id = id;
        }
    }
}