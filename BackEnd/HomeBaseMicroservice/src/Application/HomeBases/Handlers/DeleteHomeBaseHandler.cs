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
    public class DeleteHomeBaseHandler : IRequestHandler<DeleteHomeBaseCommand, Result<bool>>
    {
        private readonly IHomeBaseService _homeBaseService;
        private readonly IEventPublisher _client;

        public DeleteHomeBaseHandler(IHomeBaseService homeBaseService, IEventPublisher client)
        {
            _homeBaseService = homeBaseService;
            _client = client;
        }

        public async Task<Result<bool>> Handle(DeleteHomeBaseCommand request, CancellationToken cancellationToken)
        {
            var result = await _homeBaseService.DeleteHomeBaseAsync(request.Id);
            
            result.IfSucc(s =>
            {
                var message = new HomeBasePostMessage()
                {
                    MessageType = nameof(HomeBaseResponse),
                    Method = ApiMethod.DELETE.ToString(),
                    Message = new HomeBaseResponse
                    {
                        Id = request.Id
                    }
                };
                
                _client.PublishEvent(message);
            });

            return result;
        }
    }
}