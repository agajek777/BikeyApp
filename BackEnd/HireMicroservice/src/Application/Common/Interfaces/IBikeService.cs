using System.Threading.Tasks;
using Domain.Entities;
using LanguageExt.Common;

namespace Application.Common.Interfaces
{
    public interface IBikeService
    {
        Task<Result<bool>> CheckIfBikeAvailableAsync(string requestBikeId);
        Task<Result<bool>> CheckIfBikeExistsAsync(string requestBikeId);
        Task AddBikeAsync(Bike bike);
        Task DeleteBikeAsync(Bike bike);
        Task UpdateBikeAsync(Bike bike);
    }
}