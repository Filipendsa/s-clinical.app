using MediatR;
using S_Clinical.Domain.Interface;

namespace S_Clinical.Application.Patients.Queries
{
    public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, PatientDto>
    {
        private readonly IPatientRepository _patientRepository;

        public GetPatientByIdQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<PatientDto> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdAsync(request.Id);

            if (patient == null)
            {
                return null;
            }


            var patientDto = new PatientDto
            {
                Id = patient.Id,
                Name = patient.Name,
                PhoneNumber = patient.PhoneNumber,
                Email = patient.Email,
                Gender = patient.GenderType
            };

            return patientDto;
        }
    }
}