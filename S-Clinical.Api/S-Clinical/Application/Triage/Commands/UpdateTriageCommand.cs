using MediatR;
using S_Clinical.Domain.Enum;

namespace S_Clinical.Application.Triages.Commands
{
    public class UpdateTriageCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Symptoms { get; set; }
        public string BloodPressure { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public SpecialityTypeEnum Speciality { get; set; }
    }
}