using MediatR;
using S_Clinical.Domain.Interface;

namespace S_Clinical.Application.ClinicalCares.Commands
{
    public class UpdateClinicalCareStatusCommandHandler : IRequestHandler<UpdateClinicalCareStatusCommand, Unit>
    {
        private readonly IClinicalCareRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateClinicalCareStatusCommandHandler(IClinicalCareRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateClinicalCareStatusCommand request, CancellationToken cancellationToken)
        {
            var clinicalCare = await _repository.GetByIdAsync(request.ClinicalCareId);

            if (clinicalCare == null)
            {
                throw new System.Exception($"Atendimento {request.ClinicalCareId} não encontrado.");
            }

            clinicalCare.UpdateStatus(request.NewStatus);
            _repository.Update(clinicalCare);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}