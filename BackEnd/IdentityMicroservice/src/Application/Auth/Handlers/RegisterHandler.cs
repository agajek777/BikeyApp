using System.Threading;
using System.Threading.Tasks;
using Application.Auth.Commands;
using Application.Common.Enums;
using Application.Common.Interfaces.Communication;
using Application.Common.Interfaces.Services;
using Domain.DTOs;
using LanguageExt.Common;
using MediatR;

namespace Application.Auth.Handlers
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, Result<UserWithTokenDto>>
    {
        private readonly IAuthService _authService;
        private readonly IEventPublisher _client;

        public RegisterHandler(IAuthService authService, IEventPublisher client)
        {
            _authService = authService;
            _client = client;
        }

        public async Task<Result<UserWithTokenDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.RegisterUserAsync(request);

            result.IfSucc(s =>
            {
                var message = new UserEventMessage()
                {
                    MessageType = nameof(UserResponse),
                    Method = ApiMethod.POST.ToString(),
                    Message = new UserResponse()
                    {
                        Id = s.Id
                    }
                };
                
                _client.PublishEvent(message);
            });

            return result;
        }
    }
}