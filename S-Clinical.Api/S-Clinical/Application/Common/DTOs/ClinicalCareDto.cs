using S_Clinical.Domain.Enum;

namespace S_Clinical.Application.Patients.Queries
{
    public class ClinicalCareDetailsDto
    {
        public int Id { get; set; }
        public int SequentialNumber { get; set; }
        public DateTime DateTimeArrival { get; set; }
        public CareStatusTypeEnum Status { get; set; }
        public PatientInfo Patient { get; set; }
        public TriageInfo Triage { get; set; }

        public class PatientInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public class TriageInfo
        {
            public int Id { get; set; }
            public string Symptoms { get; set; }
            public PriorityLevelEnum Priority { get; set; }
        }
    }
}