using System.Text;
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
    public class AddHomeBaseHandler : IRequestHandler<AddHomeBaseCommand, Result<HomeBaseResponse>>
    {
        private readonly IHomeBaseService _homeBaseService;
        private readonly IEventPublisher _client;

        public AddHomeBaseHandler(IHomeBaseService homeBaseService, IEventPublisher client)
        {
            _homeBaseService = homeBaseService;
            _client = client;
        }

        public async Task<Result<HomeBaseResponse>> Handle(AddHomeBaseCommand request, CancellationToken cancellationToken)
        {
            var result = await _homeBaseService.AddHomeBaseAsync(request);
            
            result.IfSucc(s =>
            {
                var message = new HomeBasePostMessage()
                {
                    MessageType = s.GetType().Name,
                    Method = ApiMethod.POST.ToString(),
                    Message = s
                };
                
                _client.PublishEvent(message);
            });

            return result;
        }
    }
}