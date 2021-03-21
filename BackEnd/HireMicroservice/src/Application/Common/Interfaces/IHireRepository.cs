using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Hires.Commands;
using Domain.Dtos;
using LanguageExt.Common;

namespace Application.Common.Interfaces
{
    public interface IHireRepository
    {
        Task<Result<List<HireResponse>>> GetAllHiresAsync();
        Task<Result<bool>> CheckIfExistsAsync(string requestId);
        Task<Result<HireResponse>> GetHireAsync(string requestId);
        Task<Result<HireResponse>> CreateHireAsync(CreateHireCommand request);
        Task<Result<HireResponse>> UpdateHireAsync(UpdateHireCommand request);
        Task<Result<bool>> DeleteHireAsync(string requestId);
    }
}