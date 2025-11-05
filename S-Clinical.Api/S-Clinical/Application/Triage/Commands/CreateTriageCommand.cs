using MediatR;
using S_Clinical.Application.Triages.Queries; 
using S_Clinical.Domain.Enum;

namespace S_Clinical.Application.Triages.Commands
{
    public class CreateTriageCommand : IRequest<TriageDto>
    {
        public int ClinicalCareId { get; set; }
        public string Symptoms { get; set; }
        public string BloodPressure { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public PriorityLevelEnum priorityLevelType { get; set; }
        public SpecialityTypeEnum Speciality { get; set; }
    }
}