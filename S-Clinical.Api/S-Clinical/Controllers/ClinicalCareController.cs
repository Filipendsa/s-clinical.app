using MediatR;
using Microsoft.AspNetCore.Mvc;
using S_Clinical.Application.ClinicalCares.Commands;
using S_Clinical.Application.ClinicalCares.Queries;

namespace S_Clinical.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicalCareController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClinicalCareController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClinicalCare([FromBody] CreateClinicalCareCommand command)
        {
            var clinicalCareId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetClinicalCareById), new { id = clinicalCareId }, command);
        }

        [HttpGet]
        public async Task<IActionResult> GetClinicalCares()
        {
            var query = new GetAllClinicalCaresQuery();
            var clinicalCares = await _mediator.Send(query);
            return Ok(clinicalCares);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClinicalCareById(int id)
        {
            var query = new GetClinicalCareByIdQuery { Id = id };
            var clinicalCare = await _mediator.Send(query);

            return clinicalCare != null ? Ok(clinicalCare) : NotFound();
        }

        [HttpGet("next-sequential")]
        public async Task<IActionResult> GetNextSequential()
        {
            var query = new GetNextSequentialQuery();
            var nextNumber = await _mediator.Send(query);

            return Ok(new { nextNumber = nextNumber });
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateClinicalCareStatusCommand command)
        {
            if (id != command.ClinicalCareId)
            {
                return BadRequest("O ID da rota e o ID do corpo da requisição não coincidem.");
            }

            await _mediator.Send(command);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClinicalCare(int id)
        {
            var command = new DeleteClinicalCareCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("awaiting-triage")]
        public async Task<IActionResult> GetAwaitingTriage()
        {
            var query = new GetAwaitingTriageQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("completed-triage")]
        public async Task<IActionResult> GetCompletedTriage()
        {
            var query = new GetCompletedTriageQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}