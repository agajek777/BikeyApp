using Domain.DTOs;
using LanguageExt.Common;
using MediatR;

namespace Application.HomeBases.Commands
{
    public class AddHomeBaseCommand : HomeBaseCreateUpdateDto, IRequest<Result<HomeBaseResponse>>
    {
        
    }
}