using System.Collections.Generic;
using Domain.DTOs;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace Application.Bikes.Queries
{
    public class GetAllBikesQuery : IRequest<Result<List<BikeResponse>>>
    {
        
    }
}