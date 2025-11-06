using MediatR;
using Microsoft.AspNetCore.Mvc;
using S_Clinical.Application.Triages.Commands;
using S_Clinical.Application.Triages.Queries;

namespace S_Clinical.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TriageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTriage([FromBody] CreateTriageCommand command)
        {
            var triageDto = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetTriageById), new { id = triageDto.Id }, triageDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetTriage()
        {
            var query = new GetAllTriagesQuery();
            var triages = await _mediator.Send(query);
            return Ok(triages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTriageById(int id)
        {
            var query = new GetTriageByIdQuery { Id = id };
            var triage = await _mediator.Send(query);

            return triage != null ? Ok(triage) : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTriage(int id, [FromBody] UpdateTriageCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("O ID da rota e o ID do corpo da requisição não coincidem.");
            }
            await _mediator.Send(command);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTriage(int id)
        {
            var command = new DeleteTriageCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}