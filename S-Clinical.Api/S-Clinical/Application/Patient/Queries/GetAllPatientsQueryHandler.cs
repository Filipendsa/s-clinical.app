using MediatR;

using S_Clinical.Domain.Interface;

namespace S_Clinical.Application.Patients.Queries
{
    public class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, List<PatientDto>>
    {
        private readonly IPatientRepository _patientRepository;

        public GetAllPatientsQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<List<PatientDto>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {
            var patients = await _patientRepository.GetAllAsync();

            var patientDtos = patients
                .Select(p => new PatientDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    PhoneNumber = p.PhoneNumber,
                    Email = p.Email,
                    Gender = p.GenderType
                })
                .ToList();
            return patientDtos;
        }
    }
}