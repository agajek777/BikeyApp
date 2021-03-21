using System.Threading;
using System.Threading.Tasks;
using Application.Bikes.Commands;
using Application.Common.Enums;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Communication;
using Domain.DTOs;
using LanguageExt.Common;
using MediatR;
using Error = Application.Common.Errors.Error;

namespace Application.Bikes.Handlers
{
    public class UpdateBikeHandler : IRequestHandler<UpdateBikeCommand, Result<BikeResponse>>
    {
        private readonly IBikeService _bikeService;
        private readonly IEventPublisher _client;

        public UpdateBikeHandler(IBikeService bikeService, IEventPublisher client)
        {
            _bikeService = bikeService;
            _client = client;
        }

        public async Task<Result<BikeResponse>> Handle(UpdateBikeCommand request, CancellationToken cancellationToken)
        {
            var result = await _bikeService.UpdateBikeAsync(request);
            
            result.IfSucc(s =>
            {
                var message = new BikeEventMessage()
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