using MediatR;
using S_Clinical.Domain.Entities;
using S_Clinical.Domain.Interface;
using S_Clinical.Domain.Enum;
using S_Clinical.Application.Triages.Queries;

namespace S_Clinical.Application.Triages.Commands
{
    public class CreateTriageCommandHandler : IRequestHandler<CreateTriageCommand, TriageDto>
    {
        private readonly ITriageRepository _triageRepository;
        private readonly IClinicalCareRepository _clinicalCareRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTriageCommandHandler(
            ITriageRepository triageRepository,
            IClinicalCareRepository clinicalCareRepository,
            IUnitOfWork unitOfWork)
        {
            _triageRepository = triageRepository;
            _clinicalCareRepository = clinicalCareRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TriageDto> Handle(CreateTriageCommand request, CancellationToken cancellationToken)
        {
            var clinicalCare = await _clinicalCareRepository.GetByIdAsync(request.ClinicalCareId);
            if (clinicalCare == null)
            {
                throw new System.Exception($"Atendimento com ID {request.ClinicalCareId} não encontrado.");
            }

            var triage = new Triage(
                request.Symptoms,
                request.BloodPressure,
                request.Weight,
                request.Height,
                request.Speciality,
                request.priorityLevelType,
                request.ClinicalCareId
            );

            await _triageRepository.AddAsync(triage);
            clinicalCare.UpdateStatus(CareStatusTypeEnum.WAITING_CARE);
            _clinicalCareRepository.Update(clinicalCare);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new TriageDto
            {
                Id = triage.Id,
                Symptoms = triage.Symptoms,
                BloodPressure = triage.BloodPressure,
                Weight = triage.Weight,
                Height = triage.Height,
                Speciality = triage.SpecialityType,
                Priority = triage.PriorityLevelType,
                ClinicalCareId = triage.ClinicalCareId
            };
        }
    }
}