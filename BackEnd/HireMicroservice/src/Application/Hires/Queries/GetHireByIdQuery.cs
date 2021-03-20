using Domain.Dtos;
using Domain.Entities;
using LanguageExt.Common;
using MediatR;

namespace Application.Hires.Queries
{
    public class GetHireByIdQuery : IRequest<Result<HireResponse>>
    {
        public string Id { get; set; }

        public GetHireByIdQuery(string id)
        {
            Id = id;
        }
    }
}