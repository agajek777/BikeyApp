using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Services;
using Application.HomeBases.Queries;
using Domain.DTOs;
using LanguageExt.Common;
using MediatR;

namespace Application.HomeBases.Handlers
{
    public class GetAllHomeBasesHandler : IRequestHandler<GetAllHomeBasesQuery, Result<List<HomeBaseResponse>>>
    {
        private readonly IHomeBaseService _homeBaseService;

        public GetAllHomeBasesHandler(IHomeBaseService homeBaseService)
        {
            _homeBaseService = homeBaseService;
        }

        public Task<Result<List<HomeBaseResponse>>> Handle(GetAllHomeBasesQuery request, CancellationToken cancellationToken)
        {
            return _homeBaseService.GetAllHomeBasesAsync();
        }
    }
}