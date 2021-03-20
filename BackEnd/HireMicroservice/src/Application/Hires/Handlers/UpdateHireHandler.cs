using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Hires.Commands;
using Domain.Dtos;
using LanguageExt.Common;
using MediatR;

namespace Application.Hires.Handlers
{
    public class UpdateHireHandler : IRequestHandler<UpdateHireCommand, Result<HireResponse>>
    {
        private readonly IHireService _hireService;

        public UpdateHireHandler(IHireService hireService)
        {
            _hireService = hireService;
        }

        public async Task<Result<HireResponse>> Handle(UpdateHireCommand request, CancellationToken cancellationToken)
        {
            return await _hireService.UpdateHireAsync(request);
        }
    }
}