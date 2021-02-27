using System.Threading.Tasks;
using Application.Auth.Commands;
using Domain.DTOs;
using LanguageExt.Common;

namespace Application.Common.Interfaces.Services
{
    public interface IAuthService
    {
        Task<Result<UserWithTokenDto>> RegisterUserAsync(RegisterCommand request);
        Task<Result<UserWithTokenDto>> LogInUserAsync(LoginCommand request);
    }
}