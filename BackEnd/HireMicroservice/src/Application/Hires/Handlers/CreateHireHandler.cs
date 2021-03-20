using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Hires.Commands;
using Domain.Dtos;
using LanguageExt.Common;
using MediatR;

namespace Application.Hires.Handlers
{
    public class CreateHireHandler : IRequestHandler<CreateHireCommand, Result<HireResponse>>
    {
        private readonly IHireService _hireService;

        public CreateHireHandler(IHireService hireService)
        {
            _hireService = hireService;
        }

        public async Task<Result<HireResponse>> Handle(CreateHireCommand request, CancellationToken cancellationToken)
        {
            return await _hireService.CreateHireAsync(request);
        }
    }
}