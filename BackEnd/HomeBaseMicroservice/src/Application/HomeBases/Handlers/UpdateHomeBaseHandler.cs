using System.Threading;
using System.Threading.Tasks;
using Application.Common.Enums;
using Application.Common.Interfaces.Communication;
using Application.Common.Interfaces.Services;
using Application.HomeBases.Commands;
using Domain.DTOs;
using LanguageExt.Common;
using MediatR;

namespace Application.HomeBases.Handlers
{
    public class UpdateHomeBaseHandler : IRequestHandler<UpdateHomeBaseCommand, Result<HomeBaseResponse>>
    {
        private readonly IHomeBaseService _homeBaseService;
        private readonly IEventPublisher _client;

        public UpdateHomeBaseHandler(IHomeBaseService homeBaseService, IEventPublisher client)
        {
            _homeBaseService = homeBaseService;
            _client = client;
        }

        public async Task<Result<HomeBaseResponse>> Handle(UpdateHomeBaseCommand request, CancellationToken cancellationToken)
        {
            var result = await _homeBaseService.UpdateHomeBaseAsync(request);
            
            result.IfSucc(s =>
            {
                var message = new HomeBasePostMessage()
                {
                    MessageType = s.GetType().Name,
                    Method = ApiMethod.PUT.ToString(),
                    Message = s
                };
                
                _client.PublishEvent(message);
            });

            return result;
        }
    }
}