using MediatR;
using S_Clinical.Application.Patients.Queries;
using S_Clinical.Domain.Enum;
using S_Clinical.Domain.Interface;

namespace S_Clinical.Application.ClinicalCares.Queries
{
    public class GetAwaitingTriageQueryHandler : IRequestHandler<GetAwaitingTriageQuery, List<ClinicalCareDetailsDto>>
    {
        private readonly IClinicalCareRepository _repository;

        public GetAwaitingTriageQueryHandler(IClinicalCareRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ClinicalCareDetailsDto>> Handle(GetAwaitingTriageQuery request, CancellationToken cancellationToken)
        {
            var clinicalCares = await _repository.GetByStatusAsync(CareStatusTypeEnum.WAITING_TRIAGE);

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
                Triage = null
            }).ToList();

            return dtos;
        }
    }
}