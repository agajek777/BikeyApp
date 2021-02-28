using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Services;
using Application.HomeBases.Queries;
using Domain.DTOs;
using LanguageExt.Common;
using MediatR;

namespace Application.HomeBases.Handlers
{
    public class GetHomeBaseByIdHandler : IRequestHandler<GetHomeBaseByIdQuery, Result<HomeBaseResponse>>
    {
        private readonly IHomeBaseService _homeBaseService;

        public GetHomeBaseByIdHandler(IHomeBaseService homeBaseService)
        {
            _homeBaseService = homeBaseService;
        }

        public async Task<Result<HomeBaseResponse>> Handle(GetHomeBaseByIdQuery request, CancellationToken cancellationToken)
        {
            return await _homeBaseService.GetHomeBaseAsync(request.Id);
        }
    }
}