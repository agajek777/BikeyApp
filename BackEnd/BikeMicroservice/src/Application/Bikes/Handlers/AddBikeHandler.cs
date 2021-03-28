using System.Threading;
using System.Threading.Tasks;
using Application.Bikes.Commands;
using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Communication;
using Domain.DTOs;
using LanguageExt.Common;
using MediatR;

namespace Application.Bikes.Handlers
{
    public class AddBikeHandler : IRequestHandler<AddBikeCommand, Result<BikeResponse>>
    {
        private readonly IBikeService _bikeService;
        private readonly IEventPublisher _client;

        public AddBikeHandler(IBikeService bikeService, IEventPublisher client)
        {
            _bikeService = bikeService;
            _client = client;
        }

        public async Task<Result<BikeResponse>> Handle(AddBikeCommand request, CancellationToken cancellationToken)
        {
            var result = await _bikeService.AddBikeAsync(request);

            result.IfSucc(s =>
            {
                var message = new BikeEventMessage()
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