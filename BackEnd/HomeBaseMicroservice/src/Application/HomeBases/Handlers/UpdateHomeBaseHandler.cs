using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces.Services;
using Application.HomeBases.Commands;
using Domain.DTOs;
using LanguageExt.Common;
using MediatR;
using Error = Application.Common.Errors.Error;

namespace Application.HomeBases.Handlers
{
    public class UpdateHomeBaseHandler : IRequestHandler<UpdateHomeBaseCommand, Result<HomeBaseResponse>>
    {
        private readonly IHomeBaseService _homeBaseService;

        public UpdateHomeBaseHandler(IHomeBaseService homeBaseService)
        {
            _homeBaseService = homeBaseService;
        }

        public async Task<Result<HomeBaseResponse>> Handle(UpdateHomeBaseCommand request, CancellationToken cancellationToken)
        {
            return await _homeBaseService.UpdateHomeBaseAsync(request);
        }
    }
}