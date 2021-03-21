using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Hires.Commands;
using Application.Hires.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HireController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HireController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllHiresQuery();

            var outcome = await _mediator.Send(query);

            return outcome.Match<IActionResult>(s => Ok(s),
                f =>
                {
                    if (f is BadRequestException)
                        return BadRequest(f.Message);

                    if (f is InternalServerException)
                        return StatusCode(StatusCodes.Status500InternalServerError, f.Message);

                    return StatusCode(StatusCodes.Status500InternalServerError);
                });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var query = new GetHireByIdQuery(id);

            var outcome = await _mediator.Send(query);
            
            return outcome.Match<IActionResult>(s => Ok(s),
                f =>
                {
                    if (f is BadRequestException)
                        return BadRequest(f.Message);

                    if (f is InternalServerException)
                        return StatusCode(StatusCodes.Status500InternalServerError, f.Message);

                    return StatusCode(StatusCodes.Status500InternalServerError);
                });
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateHireCommand command)
        {
            var outcome = await _mediator.Send(command);
            
            return outcome.Match<IActionResult>(s => Ok(s),
                f =>
                {
                    if (f is BadRequestException)
                        return BadRequest(f.Message);

                    if (f is InternalServerException)
                        return StatusCode(StatusCodes.Status500InternalServerError, f.Message);

                    return StatusCode(StatusCodes.Status500InternalServerError);
                });
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateHireCommand command)
        {
            var outcome = await _mediator.Send(command);
            
            return outcome.Match<IActionResult>(s => Ok(s),
                f =>
                {
                    if (f is BadRequestException)
                        return BadRequest(f.Message);

                    if (f is InternalServerException)
                        return StatusCode(StatusCodes.Status500InternalServerError, f.Message);

                    return StatusCode(StatusCodes.Status500InternalServerError);
                });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteHireCommand(id);

            var outcome = await _mediator.Send(command);
            
            return outcome.Match<IActionResult>(s => Ok(s),
                f =>
                {
                    if (f is BadRequestException)
                        return BadRequest(f.Message);

                    if (f is InternalServerException)
                        return StatusCode(StatusCodes.Status500InternalServerError, f.Message);

                    return StatusCode(StatusCodes.Status500InternalServerError);
                });
        }
    }
}