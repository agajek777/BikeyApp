using System.Threading;
using System.Threading.Tasks;
using Application.Auth.Commands;
using Application.Common.Interfaces.Services;
using Domain.DTOs;
using LanguageExt.Common;
using MediatR;

namespace Application.Auth.Handlers
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, Result<UserWithTokenDto>>
    {
        private readonly IAuthService _authService;

        public RegisterHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Result<UserWithTokenDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _authService.RegisterUserAsync(request);
        }
    }
}