namespace S_Clinical.Domain.Enum
{
    public enum CareStatusTypeEnum
    {
        WAITING_TRIAGE, //0
        IN_TRIAGE,//1
        WAITING_CARE,//2
        RECEIVING_CARE,
        UNDER_OBSERVATION,
        INPATIENT,
        IN_MEDICATION,
        DISCHARGED,
        DECEASED        
    }
}