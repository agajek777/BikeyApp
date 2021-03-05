using Domain.DTOs;
using LanguageExt.Common;
using MediatR;

namespace Application.Bikes.Commands
{
    public class AddBikeCommand : BikeCreateUpdateDto, IRequest<Result<BikeResponse>>
    {
        
    }
}