using MediatR;
using S_Clinical.Domain.Interface;

namespace S_Clinical.Application.Triages.Commands
{
    public class DeleteTriageCommandHandler : IRequestHandler<DeleteTriageCommand, Unit>
    {
        private readonly ITriageRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTriageCommandHandler(ITriageRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteTriageCommand request, CancellationToken cancellationToken)
        {
            var triage = await _repository.GetByIdAsync(request.Id);
            if (triage != null)
            {
                _repository.Delete(triage);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}