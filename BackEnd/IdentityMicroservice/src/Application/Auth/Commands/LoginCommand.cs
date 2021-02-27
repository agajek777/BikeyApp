using Domain.DTOs;
using LanguageExt.Common;
using MediatR;

namespace Application.Auth.Commands
{
    public class LoginCommand : UserForLoginRegisterDto, IRequest<Result<UserWithTokenDto>>
    {
        
    }
}