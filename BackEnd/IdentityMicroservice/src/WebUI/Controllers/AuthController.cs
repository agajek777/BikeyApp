using System.Threading.Tasks;
using Application.Auth.Commands;
using Application.Common.Exceptions;
using Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForLoginRegisterDto registerDto)
        {
            var command = new RegisterCommand()
            {
                UserName = registerDto.UserName, 
                Password = registerDto.Password
            };

            var result = await _mediator.Send(command);
            
            return result.Match<IActionResult>(s => Ok(s), f =>
            {
                if (f is BadRequestException)
                    return BadRequest(f.Message);
                
                return StatusCode(StatusCodes.Status500InternalServerError);
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginRegisterDto loginDto)
        {
            var command = new LoginCommand()
            {
                UserName = loginDto.UserName, 
                Password = loginDto.Password
            };

            var result = await _mediator.Send(command);
            
            return result.Match<IActionResult>(s => Ok(s), f =>
            {
                if (f is BadRequestException)
                    return BadRequest(f.Message);
                
                return StatusCode(StatusCodes.Status500InternalServerError);
            });
        }
    }
}