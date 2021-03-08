using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IHomeBaseService
    {
        bool CheckIfExistsAsync(string requestHomeBaseId);
        Task<bool> CheckIfFreeSlotsAsync(string requestHomeBaseId);
        Task AddHomeBaseAsync(HomeBase homeBase);
    }
}