using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Bikes.Commands;
using Domain.DTOs;
using LanguageExt.Common;

namespace Application.Common.Interfaces
{
    public interface IBikeService
    {
        bool CheckIfExists(string requestId);
        Task<Result<BikeResponse>> GetBikeAsync(string requestId);
        Task<Result<List<BikeResponse>>> GetAllBikesAsync();
        Task<Result<BikeResponse>> AddBikeAsync(AddBikeCommand request);
        Task<Result<BikeResponse>> UpdateBikeAsync(UpdateBikeCommand request);
        Task<Result<bool>> DeleteBikeAsync(string requestId);
        Task<Result<List<BikeResponse>>> GetBikesInHomeBaseAsync(string requestHomeBaseId);
    }
}