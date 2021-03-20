using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Hires.Commands;
using LanguageExt.Common;
using MediatR;

namespace Application.Hires.Handlers
{
    public class DeleteHireHandler : IRequestHandler<DeleteHireCommand, Result<bool>>
    {
        private readonly IHireService _hireService;

        public DeleteHireHandler(IHireService hireService)
        {
            _hireService = hireService;
        }

        public async Task<Result<bool>> Handle(DeleteHireCommand request, CancellationToken cancellationToken)
        {
            return await _hireService.DeleteHireAsync(request.Id);
        }
    }
}