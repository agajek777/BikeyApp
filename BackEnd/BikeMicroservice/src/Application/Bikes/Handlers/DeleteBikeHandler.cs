using System.Threading;
using System.Threading.Tasks;
using Application.Bikes.Commands;
using Application.Common.Enums;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Communication;
using Domain.DTOs;
using Domain.Entities;
using LanguageExt.Common;
using MediatR;
using Error = Application.Common.Errors.Error;

namespace Application.Bikes.Handlers
{
    public class DeleteBikeHandler : IRequestHandler<DeleteBikeCommand, Result<bool>>
    {
        private readonly IEventPublisher _client;
        private readonly IBikeService _bikeService;

        public DeleteBikeHandler(IBikeService bikeService, IEventPublisher client)
        {
            _client = client;
            _bikeService = bikeService;
        }

        public async Task<Result<bool>> Handle(DeleteBikeCommand request, CancellationToken cancellationToken)
        {
            var result = await _bikeService.DeleteBikeAsync(request.Id);
            
            result.IfSucc(s =>
            {
                var message = new BikeEventMessage()
                {
                    MessageType = nameof(BikeResponse),
                    Method = ApiMethod.DELETE.ToString(),
                    Message = new BikeResponse()
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