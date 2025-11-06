using MediatR;
using S_Clinical.Domain.Enum;
using S_Clinical.Domain.Interface;

namespace S_Clinical.Application.ClinicalCares.Queries
{
    public class GetQueueSummaryQueryHandler : IRequestHandler<GetQueueSummaryQuery, QueueSummaryDto>
    {
        private readonly IClinicalCareRepository _repository;

        public GetQueueSummaryQueryHandler(IClinicalCareRepository repository)
        {
            _repository = repository;
        }

        public async Task<QueueSummaryDto> Handle(GetQueueSummaryQuery request, CancellationToken cancellationToken)
        {
            var awaitingTriageCount = await _repository.GetCountByStatusAsync(CareStatusTypeEnum.WAITING_TRIAGE, false);
            var awaitingCareCount = await _repository.GetCountByStatusAsync(CareStatusTypeEnum.WAITING_CARE,false);

            return new QueueSummaryDto
            {
                AwaitingTriageCount = awaitingTriageCount,
                AwaitingCareCount = awaitingCareCount
            };
        }
    }
}