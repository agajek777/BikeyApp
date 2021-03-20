using Domain.Dtos;
using LanguageExt.Common;
using MediatR;

namespace Application.Hires.Commands
{
    public class CreateHireCommand : HireForCreateUpdateDto, IRequest<Result<HireResponse>>
    {
    }
}