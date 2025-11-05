using MediatR;
using S_Clinical.Domain.Interface;
using System.Threading;
using System.Threading.Tasks;

namespace S_Clinical.Application.ClinicalCares.Queries
{
    public class GetNextSequentialQueryHandler : IRequestHandler<GetNextSequentialQuery, int>
    {
        private readonly IClinicalCareRepository _repository;

        public GetNextSequentialQueryHandler(IClinicalCareRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(GetNextSequentialQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetNextSequentialNumberAsync();
        }
    }
}