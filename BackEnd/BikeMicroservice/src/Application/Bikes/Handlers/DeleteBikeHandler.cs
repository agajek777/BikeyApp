using System.Threading;
using System.Threading.Tasks;
using Application.Bikes.Commands;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.DTOs;
using LanguageExt.Common;
using MediatR;
using Error = Application.Common.Errors.Error;

namespace Application.Bikes.Handlers
{
    public class DeleteBikeHandler : IRequestHandler<DeleteBikeCommand, Result<bool>>
    {
        private readonly IBikeService _bikeService;

        public DeleteBikeHandler(IBikeService bikeService)
        {
            _bikeService = bikeService;
        }

        public async Task<Result<bool>> Handle(DeleteBikeCommand request, CancellationToken cancellationToken)
        {
            return await _bikeService.DeleteBikeAsync(request.Id);
        }
    }
}