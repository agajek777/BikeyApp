using LanguageExt.Common;
using MediatR;

namespace Application.HomeBases.Commands
{
    public class DeleteHomeBaseCommand : IRequest<Result<bool>>
    {
        public string Id { get; set; }

        public DeleteHomeBaseCommand(string id)
        {
            Id = id;
        }
    }
}