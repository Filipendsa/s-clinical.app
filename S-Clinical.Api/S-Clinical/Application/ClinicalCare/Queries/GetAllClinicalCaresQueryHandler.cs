using MediatR;
using S_Clinical.Application.ClinicalCares.Queries;
using S_Clinical.Domain.Interface;

namespace S_Clinical.Application.Patients.Queries
{
    public class GetAllClinicalCaresQueryHandler : IRequestHandler<GetAllClinicalCaresQuery, List<ClinicalCareDetailsDto>>
    {
        private readonly IClinicalCareRepository _clinicalCareRepository;

        public GetAllClinicalCaresQueryHandler(IClinicalCareRepository clinicalCareRepository)
        {
            _clinicalCareRepository = clinicalCareRepository;
        }

        public async Task<List<ClinicalCareDetailsDto>> Handle(GetAllClinicalCaresQuery request, CancellationToken cancellationToken)
        {
            var clinicalCares = await _clinicalCareRepository.GetAllAsync();

            var clinicalCareDtos = clinicalCares
                .Select(c => new ClinicalCareDetailsDto
                {
                    Id = c.Id,
                    SequentialNumber = c.SequentialNumber,
                    DateTimeArrival = c.DateTimeArrival,
                    Status = c.StatusType,
                    Patient = new ClinicalCareDetailsDto.PatientInfo
                    {
                        Id = c.Patient.Id,
                        Name = c.Patient.Name
                    },
                    Triage = c.Triage == null ? null : new ClinicalCareDetailsDto.TriageInfo
                    {
                        Id = c.Triage.Id,
                        Symptoms = c.Triage.Symptoms,
                        Priority = c.Triage.PriorityLevelType
                    }
                })
                .ToList();
            return clinicalCareDtos;
        }
    }
}