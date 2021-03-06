using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IHomeBaseService
    {
        bool CheckIfExistsAsync(string requestHomeBaseId);
        Task<bool> CheckIfFreeSlotsAsync(string requestHomeBaseId);
    }
}