using System.Threading.Tasks;
using Application.Bikes.Commands;
using Application.Bikes.Queries;
using Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BikeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BikeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var query = new GetBikeById(id);
            
            return (await _mediator.Send(query)).Match<IActionResult>(
                s => Ok(s),
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
            var query = new GetAllBikesQuery();

            return (await _mediator.Send(query)).Match<IActionResult>(
                s => Ok(s),
                f =>
                {
                    if (f is BadRequestException)
                        return BadRequest(f.Message);

                    return StatusCode(StatusCodes.Status500InternalServerError);
                });
        }
        
        [HttpGet("GetBikesInHb/{homeBaseId}")]
        public async Task<IActionResult> GetBikesInHomeBase(string homeBaseId)
        {
            var query = new GetBikesByHomeBaseId(homeBaseId);

            return (await _mediator.Send(query)).Match<IActionResult>(
                s => Ok(s),
                f =>
                {
                    if (f is BadRequestException)
                        return BadRequest(f.Message);

                    return StatusCode(StatusCodes.Status500InternalServerError);
                });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddBikeCommand command)
        {
            return (await _mediator.Send(command)).Match<IActionResult>(
                s => Ok(s),
                f =>
                {
                    if (f is BadRequestException)
                        return BadRequest(f.Message);

                    return StatusCode(StatusCodes.Status500InternalServerError);
                });
        }
        
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateBikeCommand command)
        {
            return (await _mediator.Send(command)).Match<IActionResult>(
                s => Ok(s),
                f =>
                {
                    if (f is BadRequestException)
                        return BadRequest(f.Message);

                    return StatusCode(StatusCodes.Status500InternalServerError);
                });
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteBikeCommand(id);

            return (await _mediator.Send(command)).Match<IActionResult>(
                s => Ok(s),
                f =>
                {
                    if (f is BadRequestException)
                        return BadRequest(f.Message);

                    return StatusCode(StatusCodes.Status500InternalServerError);
                });
        }
    }
}