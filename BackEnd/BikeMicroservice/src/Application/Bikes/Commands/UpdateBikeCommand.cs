using Domain.DTOs;
using LanguageExt.Common;
using MediatR;

namespace Application.Bikes.Commands
{
    public class UpdateBikeCommand : BikeCreateUpdateDto, IRequest<Result<BikeResponse>>
    {
        public string Id { get; set; }
    }
}