using System.Collections.Generic;
using System.Threading.Tasks;
using Application.HomeBases.Commands;
using Domain.DTOs;
using LanguageExt.Common;

namespace Application.Common.Interfaces.Services
{
    public interface IHomeBaseService
    {
        Task<bool> CheckIfUniqueAsync(string name);
        Task<Result<HomeBaseResponse>> AddHomeBaseAsync(AddHomeBaseCommand request);
        Task<Result<HomeBaseResponse>> GetHomeBaseAsync(string requestId);
        Task<Result<List<HomeBaseResponse>>> GetAllHomeBasesAsync();
        Task<bool> CheckIfExistsAsync(string requestId);
        Task<Result<HomeBaseResponse>> UpdateHomeBaseAsync(UpdateHomeBaseCommand request);
        Task<Result<bool>> DeleteHomeBaseAsync(string requestId);
    }
}