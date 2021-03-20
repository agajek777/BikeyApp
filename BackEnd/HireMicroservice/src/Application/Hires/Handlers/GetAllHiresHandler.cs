using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Hires.Queries;
using Domain.Dtos;
using LanguageExt.Common;
using MediatR;

namespace Application.Hires.Handlers
{
    public class GetAllHiresHandler : IRequestHandler<GetAllHiresQuery, Result<List<HireResponse>>>
    {
        private readonly IHireService _hireService;

        public GetAllHiresHandler(IHireService hireService)
        {
            _hireService = hireService;
        }

        public async Task<Result<List<HireResponse>>> Handle(GetAllHiresQuery request, CancellationToken cancellationToken)
        {
            return await _hireService.GetAllHiresAsync();
        }
    }
}