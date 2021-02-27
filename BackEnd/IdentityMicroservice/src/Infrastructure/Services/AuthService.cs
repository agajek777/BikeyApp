using System.Linq;
using System.Threading.Tasks;
using Application.Auth.Commands;
using Application.Common.Exceptions;
using Application.Common.Interfaces.Services;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using LanguageExt.Common;
using Microsoft.AspNetCore.Identity;
using Error = Application.Common.Errors.Error;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public AuthService(UserManager<User> userManager, IMapper mapper, IJwtService jwtService, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtService = jwtService;
            _signInManager = signInManager;
        }

        public async Task<Result<UserWithTokenDto>> RegisterUserAsync(RegisterCommand request)
        {
            var user = new User() {UserName = request.UserName};

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return new Result<UserWithTokenDto>(new BadRequestException(result.Errors.First().Description));

            var outcome = _mapper.Map<UserWithTokenDto>(user);

            outcome.AccessToken = (await _jwtService.GenerateTokenAsync(user)).ToString();

            return new Result<UserWithTokenDto>(outcome);
        }

        public async Task<Result<UserWithTokenDto>> LogInUserAsync(LoginCommand request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
                return new Result<UserWithTokenDto>(new BadRequestException(Error.UserNotFound));

            if (request.UserName != user.UserName)
                return new Result<UserWithTokenDto>(new BadRequestException(Error.InvalidUsernameOrPassword));

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if (!result.Succeeded)
                return new Result<UserWithTokenDto>(new BadRequestException(Error.InvalidUsernameOrPassword));

            var outcome = _mapper.Map<UserWithTokenDto>(user);

            outcome.AccessToken = (await _jwtService.GenerateTokenAsync(user)).ToString();

            return new Result<UserWithTokenDto>(outcome);
        }
    }
}