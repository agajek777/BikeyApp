using Domain.DTOs;
using LanguageExt.Common;
using MediatR;

namespace Application.HomeBases.Commands
{
    public class UpdateHomeBaseCommand : HomeBaseCreateUpdateDto, IRequest<Result<HomeBaseResponse>>
    {
        public string Id { get; set; }
    }
}