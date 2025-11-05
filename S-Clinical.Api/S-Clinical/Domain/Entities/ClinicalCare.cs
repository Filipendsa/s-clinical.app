using S_Clinical.Domain.Enum;

namespace S_Clinical.Domain.Entities
{
    public class ClinicalCare
    {
        public int Id { get; private set; }
        public int SequentialNumber { get; private set; } = 0;
        public DateTime DateTimeArrival { get; private set; } = DateTime.UtcNow;
        public CareStatusTypeEnum StatusType { get; private set; } = CareStatusTypeEnum.WAITING_TRIAGE;
        public int PatientId { get; private set; } = 0;
        public Patient Patient { get; private set; }
        public Triage Triage { get; private set; }


        public ClinicalCare(int sequentialNumber, int patientId)
        {
            SequentialNumber = sequentialNumber;
            PatientId = patientId;
            DateTimeArrival = DateTime.UtcNow;
            StatusType = CareStatusTypeEnum.WAITING_TRIAGE;
        }

        public void UpdateStatus(CareStatusTypeEnum newStatus)
        {
            this.StatusType = newStatus;
        }
    }
}
