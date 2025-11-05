using MediatR;
using S_Clinical.Domain.Interface;

namespace S_Clinical.Application.Triages.Queries
{
    public class GetAllTriagesQueryHandler : IRequestHandler<GetAllTriagesQuery, List<TriageDto>>
    {
        private readonly ITriageRepository _triageRepository;

        public GetAllTriagesQueryHandler(ITriageRepository triageRepository)
        {
            _triageRepository = triageRepository;
        }

        public async Task<List<TriageDto>> Handle(GetAllTriagesQuery request, CancellationToken cancellationToken)
        {
            var clinicalCares = await _triageRepository.GetAllAsync();

            var clinicalCareDtos = clinicalCares
                .Select(t => new TriageDto
                {
                    Id = t.Id,
                    Symptoms = t.Symptoms,
                    BloodPressure = t.BloodPressure,
                    Height = t.Height,
                    Speciality = t.SpecialityType,
                    Priority = t.PriorityLevelType,
                    ClinicalCareId = t.ClinicalCareId
    })
                .ToList();
            return clinicalCareDtos;
        }
    }
}