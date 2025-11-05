using MediatR;
namespace S_Clinical.Application.Triages.Commands
{
    public class DeleteTriageCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}