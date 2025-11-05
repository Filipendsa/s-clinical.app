using MediatR;
using S_Clinical.Domain.Enum;

namespace S_Clinical.Application.ClinicalCares.Commands
{
    public class UpdateClinicalCareStatusCommand : IRequest<Unit>
    {
        public int ClinicalCareId { get; set; }
        public CareStatusTypeEnum NewStatus { get; set; }
    }
}