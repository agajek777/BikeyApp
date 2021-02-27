using System.Threading.Tasks;
using Application.Auth.Commands;
using Application.Common.Exceptions;
using Application.Common.Interfaces.Services;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using LanguageExt.Common;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AuthService(UserManager<User> userManager, IMapper mapper, IJwtService jwtService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<Result<UserWithTokenDto>> RegisterUserAsync(RegisterCommand request)
        {
            var user = new User() {UserName = request.UserName};

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return new Result<UserWithTokenDto>(new BadRequestException(result.Errors.ToString()));

            var outcome = _mapper.Map<UserWithTokenDto>(user);

            outcome.AccessToken = (await _jwtService.GenerateTokenAsync(user)).ToString();

            return new Result<UserWithTokenDto>(outcome);
        }

        public Task<Result<UserWithTokenDto>> LogInUserAsync(LoginCommand request)
        {
            throw new System.NotImplementedException();
        }
    }
}