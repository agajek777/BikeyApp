using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.HomeBases.Commands;
using Application.HomeBases.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeBaseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HomeBaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var query = new GetHomeBaseByIdQuery(id);

            var result = await _mediator.Send(query);

            return result.Match<IActionResult>(s => Ok(s),
                f =>
                {
                    if (f is BadRequestException)
                        return BadRequest(f.Message);

                    return StatusCode(StatusCodes.Status500InternalServerError);
                });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllHomeBasesQuery();

            var result = await _mediator.Send(query);

            return result.Match<IActionResult>(s => Ok(s),
                f =>
                {
                    if (f is BadRequestException)
                        return BadRequest(f.Message);

                    return StatusCode(StatusCodes.Status500InternalServerError);
                });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddHomeBaseCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Match<IActionResult>(s => Ok(s),
                f =>
                {
                    if (f is BadRequestException)
                        return BadRequest(f.Message);

                    return StatusCode(StatusCodes.Status500InternalServerError);
                });
        }
        
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateHomeBaseCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Match<IActionResult>(s => Ok(s),
                f =>
                {
                    if (f is BadRequestException)
                        return BadRequest(f.Message);

                    return StatusCode(StatusCodes.Status500InternalServerError);
                });
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            var command = new DeleteHomeBaseCommand(id);
            
            var result = await _mediator.Send(command);

            return result.Match<IActionResult>(s => Ok(s),
                f =>
                {
                    if (f is BadRequestException)
                        return BadRequest(f.Message);

                    return StatusCode(StatusCodes.Status500InternalServerError);
                });
        }
    }
}