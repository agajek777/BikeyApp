using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces.Services;
using Application.HomeBases.Commands;
using LanguageExt.Common;
using MediatR;

namespace Application.HomeBases.Handlers
{
    public class DeleteHomeBaseHandler : IRequestHandler<DeleteHomeBaseCommand, Result<bool>>
    {
        private readonly IHomeBaseService _homeBaseService;

        public DeleteHomeBaseHandler(IHomeBaseService homeBaseService)
        {
            _homeBaseService = homeBaseService;
        }

        public async Task<Result<bool>> Handle(DeleteHomeBaseCommand request, CancellationToken cancellationToken)
        {
            return await _homeBaseService.DeleteHomeBaseAsync(request.Id);
        }
    }
}