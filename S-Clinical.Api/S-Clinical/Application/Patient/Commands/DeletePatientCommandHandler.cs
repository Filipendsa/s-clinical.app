using MediatR;
using S_Clinical.Domain.Interface;


public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, Unit>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePatientCommandHandler(IPatientRepository patientRepository, IUnitOfWork unitOfWork)
    {
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(request.Id);

        if (patient == null)
        {
            throw new System.Exception($"Paciente com ID {request.Id} não encontrado.");
        }

         _patientRepository.Delete(patient);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}