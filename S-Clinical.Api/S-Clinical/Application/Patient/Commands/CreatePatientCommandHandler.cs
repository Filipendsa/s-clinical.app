using MediatR;
using S_Clinical.Domain.Entities;
using S_Clinical.Domain.Interface;


public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, int>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePatientCommandHandler(IPatientRepository patientRepository, IUnitOfWork unitOfWork)
    {
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = new Patient(
            request.Name,
            request.PhoneNumber,
            request.Email,
            request.Gender
        );

        await _patientRepository.AddAsync(patient);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return patient.Id;
    }
}