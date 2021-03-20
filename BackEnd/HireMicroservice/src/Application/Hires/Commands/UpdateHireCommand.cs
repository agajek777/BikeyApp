using Domain.Dtos;
using LanguageExt.Common;
using MediatR;

namespace Application.Hires.Commands
{
    public class UpdateHireCommand : HireForCreateUpdateDto, IRequest<Result<HireResponse>>
    {
        public UpdateHireCommand(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}