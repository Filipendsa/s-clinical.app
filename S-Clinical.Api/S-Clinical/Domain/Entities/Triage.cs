using S_Clinical.Domain.Enum;

namespace S_Clinical.Domain.Entities 
{
    public class Triage
    {
        private SpecialityTypeEnum speciality;

        public int Id { get; private set; }
        public string Symptoms { get; private set; } = string.Empty;
        public string BloodPressure { get; private set; } = string.Empty;
        public decimal Weight { get; private set; } = 0;
        public decimal Height { get; private set; } = 0;
        public SpecialityTypeEnum SpecialityType { get; private set; } = SpecialityTypeEnum.CLINICAL_MEDICINE;
        public PriorityLevelEnum PriorityLevelType { get; private set; } = PriorityLevelEnum.BLUE;
        public int ClinicalCareId { get; private set; }
        public ClinicalCare ClinicalCare { get; private set; }

        public Triage(
            string symptoms,
            string bloodPressure,
            decimal weight,
            decimal height,
            SpecialityTypeEnum specialityType,
            PriorityLevelEnum priorityLevelType,
            int clinicalCareId)
        {
            if (string.IsNullOrWhiteSpace(symptoms))
                throw new ArgumentException("Symptoms are required.", nameof(symptoms));

            Symptoms = symptoms;
            BloodPressure = bloodPressure;
            Weight = weight;
            Height = height;
            SpecialityType = specialityType;
            PriorityLevelType = priorityLevelType;
            ClinicalCareId = clinicalCareId;

            //EvaluatePriority(); TODO: Ideia de categorizacao automatica revalidar
        }

        private void EvaluatePriority()
        {
            if (Symptoms.Contains("dor no peito", StringComparison.OrdinalIgnoreCase) ||
                Symptoms.Contains("falta de ar", StringComparison.OrdinalIgnoreCase) ||
                Symptoms.Contains("inconsciente", StringComparison.OrdinalIgnoreCase))
            {
                this.PriorityLevelType = PriorityLevelEnum.RED;
            }
            else if (Symptoms.Contains("fratura exposta", StringComparison.OrdinalIgnoreCase) ||
                     Symptoms.Contains("sangramento intenso", StringComparison.OrdinalIgnoreCase))
            {
                this.PriorityLevelType = PriorityLevelEnum.ORANGE;
            }
            else if (Symptoms.Contains("febre alta", StringComparison.OrdinalIgnoreCase) ||
                     Symptoms.Contains("vomito persistente", StringComparison.OrdinalIgnoreCase))
            {
                this.PriorityLevelType = PriorityLevelEnum.YELLOW;
            }
            else if (Symptoms.Contains("resfriado", StringComparison.OrdinalIgnoreCase) ||
                     Symptoms.Contains("torcao leve", StringComparison.OrdinalIgnoreCase))
            {
                this.PriorityLevelType = PriorityLevelEnum.GREEN;
            }
            else
            {
                this.PriorityLevelType = PriorityLevelEnum.BLUE;
            }
        }
    }
}