﻿using Domain.DTOs;
using LanguageExt.Common;
using MediatR;

namespace Application.Auth.Commands
{
    public class RegisterCommand : UserForLoginRegisterDto, IRequest<Result<UserWithTokenDto>>
    {
        
    }
}