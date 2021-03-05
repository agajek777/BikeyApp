using LanguageExt.Common;
using MediatR;

namespace Application.Bikes.Commands
{
    public class DeleteBikeCommand : IRequest<Result<bool>>
    {
        public string Id { get; set; }

        public DeleteBikeCommand(string id)
        {
            Id = id;
        }
    }
}