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
    public class AddHomeBaseHandler : IRequestHandler<AddHomeBaseCommand, Result<HomeBaseResponse>>
    {
        private readonly IHomeBaseService _homeBaseService;

        public AddHomeBaseHandler(IHomeBaseService homeBaseService)
        {
            _homeBaseService = homeBaseService;
        }

        public async Task<Result<HomeBaseResponse>> Handle(AddHomeBaseCommand request, CancellationToken cancellationToken)
        {
            if (!await _homeBaseService.CheckIfUniqueAsync(request.Name))
                return new Result<HomeBaseResponse>(new BadRequestException(Error.InvalidHomeBaseName));

            return await _homeBaseService.AddHomeBaseAsync(request);
        }
    }
}