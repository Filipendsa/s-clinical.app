using Microsoft.AspNetCore.Mvc;
using MediatR;
using S_Clinical.Application.Patients.Queries;
using S_Clinical.Application.Patients.Commands;

namespace S_Clinical.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PatientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatientCommand command)
        {
            var patientId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetPatientById), new { id = patientId }, command);
        }


        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            var query = new GetAllPatientsQuery();
            var patients = await _mediator.Send(query);
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            GetPatientByIdQuery query = new GetPatientByIdQuery { Id = id };
            var patient = await _mediator.Send(query);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] UpdatePatientCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("O ID da rota e o ID do corpo da requisição não coincidem.");
            }

            var result = await _mediator.Send(command);


            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var command = new DeletePatientCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}