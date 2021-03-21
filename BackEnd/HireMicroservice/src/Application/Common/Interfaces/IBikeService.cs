using System.Threading.Tasks;
using LanguageExt.Common;

namespace Application.Common.Interfaces
{
    public interface IBikeService
    {
        Task<Result<bool>> CheckIfBikeAvailableAsync(string requestBikeId);
        Task<Result<bool>> CheckIfBikeExistsAsync(string requestBikeId);
    }
}