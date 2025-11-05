using S_Clinical.Domain.Enum;

namespace S_Clinical.Application.Patients.Queries
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public GenderTypeEnum Gender { get; set; }
    }
}