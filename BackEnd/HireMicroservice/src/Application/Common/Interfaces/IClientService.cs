using System.Threading.Tasks;
using LanguageExt.Common;

namespace Application.Common.Interfaces
{
    public interface IClientService
    {
        Task<Result<bool>> CheckIfClientAvailableAsync(string requestClientId);
        Task<Result<bool>> CheckIfExistsAsync(string requestClientId);
    }
}