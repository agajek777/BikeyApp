using System.Threading.Tasks;
using Domain.Entities;
using LanguageExt.Common;

namespace Application.Common.Interfaces
{
    public interface IClientService
    {
        Task<Result<bool>> CheckIfClientAvailableAsync(string requestClientId);
        Task<Result<bool>> CheckIfExistsAsync(string requestClientId);
        Task AddClientAsync(Client client);
        Task DeleteClientAsync(Client client);
    }
}