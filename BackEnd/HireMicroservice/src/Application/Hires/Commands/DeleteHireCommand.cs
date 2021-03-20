using LanguageExt.Common;
using MediatR;

namespace Application.Hires.Commands
{
    public class DeleteHireCommand : IRequest<Result<bool>>
    {
        public DeleteHireCommand(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}