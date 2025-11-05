using MediatR;
namespace S_Clinical.Application.Triages.Queries
{
    public class GetTriageByIdQuery : IRequest<TriageDto>
    {
        public int Id { get; set; }
    }
}