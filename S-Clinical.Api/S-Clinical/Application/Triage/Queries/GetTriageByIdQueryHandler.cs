using MediatR;
using S_Clinical.Domain.Interface;

namespace S_Clinical.Application.Triages.Queries
{
    public class GetTriageByIdQueryHandler : IRequestHandler<GetTriageByIdQuery, TriageDto>
    {
        private readonly ITriageRepository _triageRepository;

        public GetTriageByIdQueryHandler(ITriageRepository triageRepository)
        {
            _triageRepository = triageRepository;
        }

        public async Task<TriageDto> Handle(GetTriageByIdQuery request, CancellationToken cancellationToken)
        {
            var triage = await _triageRepository.GetByIdAsync(request.Id);
            if (triage == null) return null;

            return new TriageDto
            {
                Id = triage.Id,
                Symptoms = triage.Symptoms,
                BloodPressure = triage.BloodPressure,
                Weight = triage.Weight,
                Height = triage.Height,
                Speciality = triage.SpecialityType,
                Priority = triage.PriorityLevelType,
                ClinicalCareId = triage.ClinicalCareId
            };
        }
    }
}