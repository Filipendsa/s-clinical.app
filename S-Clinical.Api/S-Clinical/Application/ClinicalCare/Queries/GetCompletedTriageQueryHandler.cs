using MediatR;
using S_Clinical.Application.Patients.Queries;
using S_Clinical.Domain.Interface;


namespace S_Clinical.Application.ClinicalCares.Queries
{
    public class GetCompletedTriageQueryHandler : IRequestHandler<GetCompletedTriageQuery, List<ClinicalCareDetailsDto>>
    {
        private readonly IClinicalCareRepository _repository;

        public GetCompletedTriageQueryHandler(IClinicalCareRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ClinicalCareDetailsDto>> Handle(GetCompletedTriageQuery request, CancellationToken cancellationToken)
        {
            var clinicalCares = await _repository.GetCompletedAsync();

            var dtos = clinicalCares.Select(care => new ClinicalCareDetailsDto
            {
                Id = care.Id,
                SequentialNumber = care.SequentialNumber,
                DateTimeArrival = care.DateTimeArrival,
                Status = care.StatusType,
                Patient = care.Patient == null ? null : new ClinicalCareDetailsDto.PatientInfo
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
            }).ToList();

            return dtos;
        }
    }
}