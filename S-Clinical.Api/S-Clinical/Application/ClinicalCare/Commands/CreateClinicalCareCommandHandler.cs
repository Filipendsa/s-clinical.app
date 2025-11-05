using MediatR;
using S_Clinical.Domain.Entities;
using S_Clinical.Domain.Interface;

namespace S_Clinical.Application.ClinicalCares.Commands
{
    public class CreateClinicalCareCommandHandler : IRequestHandler<CreateClinicalCareCommand, int>
    {
        private readonly IClinicalCareRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateClinicalCareCommandHandler(IClinicalCareRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateClinicalCareCommand request, CancellationToken cancellationToken)
        {
            var clinicalCare = new ClinicalCare(request.SequentialNumber, request.PatientId);

            await _repository.AddAsync(clinicalCare);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return clinicalCare.Id;
        }
    }
}