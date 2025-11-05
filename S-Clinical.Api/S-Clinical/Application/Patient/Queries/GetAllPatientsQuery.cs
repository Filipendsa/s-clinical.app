using MediatR;

namespace S_Clinical.Application.Patients.Queries
{
    public class GetAllPatientsQuery : IRequest<List<PatientDto>>
    {

    }
}