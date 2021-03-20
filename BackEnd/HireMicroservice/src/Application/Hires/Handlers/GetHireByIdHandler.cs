using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Hires.Queries;
using Domain.Dtos;
using LanguageExt.Common;
using MediatR;

namespace Application.Hires.Handlers
{
    public class GetHireByIdHandler : IRequestHandler<GetHireByIdQuery, Result<HireResponse>>
    {
        private readonly IHireService _hireService;

        public GetHireByIdHandler(IHireService hireService)
        {
            _hireService = hireService;
        }

        public async Task<Result<HireResponse>> Handle(GetHireByIdQuery request, CancellationToken cancellationToken)
        {
            return await _hireService.GetHireAsync(request.Id);
        }
    }
}