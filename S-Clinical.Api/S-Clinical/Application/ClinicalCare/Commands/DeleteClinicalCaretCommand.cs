using MediatR;
namespace S_Clinical.Application.ClinicalCares.Commands
{
    public class DeleteClinicalCareCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}