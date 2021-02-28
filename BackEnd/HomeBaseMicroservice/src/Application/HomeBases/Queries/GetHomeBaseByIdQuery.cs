using Domain.DTOs;
using LanguageExt.Common;
using MediatR;

namespace Application.HomeBases.Queries
{
    public class GetHomeBaseByIdQuery : IRequest<Result<HomeBaseResponse>>
    {
        public string Id { get; set; }

        public GetHomeBaseByIdQuery(string id)
        {
            Id = id;
        }
    }
}