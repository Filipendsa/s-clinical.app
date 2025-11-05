using MediatR;
using S_Clinical.Domain.Interface;


namespace S_Clinical.Application.Patients.Commands
{
    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, Unit>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePatientCommandHandler(IPatientRepository patientRepository, IUnitOfWork unitOfWork)
        {
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdAsync(request.Id);

            if (patient == null)
            {
                throw new System.Exception($"Paciente com ID {request.Id} não encontrado.");
            }

            patient.Update(request.PhoneNumber, request.Email,request.Name,request.Gender);
            _patientRepository.Update(patient);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}