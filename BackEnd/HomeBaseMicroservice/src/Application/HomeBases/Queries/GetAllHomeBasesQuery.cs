using System.Collections.Generic;
using Domain.DTOs;
using LanguageExt.Common;
using MediatR;

namespace Application.HomeBases.Queries
{
    public class GetAllHomeBasesQuery : IRequest<Result<List<HomeBaseResponse>>>
    {
        
    }
}