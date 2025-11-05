using MediatR;
using S_Clinical.Domain.Interface;

namespace S_Clinical.Application.ClinicalCares.Commands
{
    public class DeleteClinicalCareCommandHandler : IRequestHandler<DeleteClinicalCareCommand, Unit>
    {
        private readonly IClinicalCareRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteClinicalCareCommandHandler(IClinicalCareRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteClinicalCareCommand request, CancellationToken cancellationToken)
        {
            var clinicalCare = await _repository.GetByIdAsync(request.Id);
            if (clinicalCare != null)
            {
                _repository.Delete(clinicalCare);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}