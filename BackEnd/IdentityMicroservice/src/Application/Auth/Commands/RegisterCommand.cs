using Domain.DTOs;
using MediatR;

namespace Application.Auth.Commands
{
    public class RegisterCommand : UserForLoginRegisterDto, IRequest<UserWithTokenDto>
    {
        
    }
}