using MediatR;
using S_Clinical.Application.Patients.Queries;
using S_Clinical.Domain.Interface;

namespace S_Clinical.Application.ClinicalCares.Queries
{
    public class GetClinicalCareByIdQueryHandler : IRequestHandler<GetClinicalCareByIdQuery, ClinicalCareDetailsDto>
    {
        private readonly IClinicalCareRepository _repository;
        public GetClinicalCareByIdQueryHandler(IClinicalCareRepository repository)
        {
            _repository = repository;
        }

        public async Task<ClinicalCareDetailsDto> Handle(GetClinicalCareByIdQuery request, CancellationToken cancellationToken)
        {
            var care = await _repository.GetByIdAsync(request.Id);
            if (care == null) return null;

            // Mapeamento Manual
            return new ClinicalCareDetailsDto
            {
                Id = care.Id,
                SequentialNumber = care.SequentialNumber,
                DateTimeArrival = care.DateTimeArrival,
                Status = care.StatusType,
                Patient = new ClinicalCareDetailsDto.PatientInfo
                {
                    Id = care.Patient.Id,
                    Name = care.Patient.Name
                },
                Triage = care.Triage == null ? null : new ClinicalCareDetailsDto.TriageInfo
                {
                    Id = care.Triage.Id,
                    Symptoms = care.Triage.Symptoms,
                    Priority = care.Triage.PriorityLevelType
                }
            };
        }
    }
}