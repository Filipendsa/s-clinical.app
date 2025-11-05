using S_Clinical.Domain.Enum;

namespace S_Clinical.Application.Triages.Queries
{

    public class TriageDto
    {
        public int Id { get; set; }
        public string Symptoms { get; set; }
        public string BloodPressure { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public SpecialityTypeEnum Speciality { get; set; }
        public PriorityLevelEnum Priority { get; set; }
        public int ClinicalCareId { get; set; }
    }
}