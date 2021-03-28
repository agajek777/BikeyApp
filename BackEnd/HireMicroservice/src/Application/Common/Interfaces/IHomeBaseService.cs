using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IHomeBaseService
    {
        Task AddHomeBaseAsync(HomeBase homeBase);
        Task DeleteHomeBaseAsync(HomeBase homeBase);
        Task UpdateHomeBaseAsync(HomeBaseUpdateDto homeBase);
    }
}