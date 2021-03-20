using System.Collections.Generic;
using Domain.Dtos;
using Domain.Entities;
using LanguageExt.Common;
using MediatR;

namespace Application.Hires.Queries
{
    public class GetAllHiresQuery : IRequest<Result<List<HireResponse>>>
    {
    }
}