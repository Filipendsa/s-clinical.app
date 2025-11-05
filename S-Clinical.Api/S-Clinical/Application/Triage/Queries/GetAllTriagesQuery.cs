using MediatR;

namespace S_Clinical.Application.Triages.Queries
{
    public class GetAllTriagesQuery : IRequest<List<TriageDto>>
    {

    }
}