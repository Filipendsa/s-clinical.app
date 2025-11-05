using MediatR;
using S_Clinical.Application.Patients.Queries;

namespace S_Clinical.Application.ClinicalCares.Queries
{
    public class GetAllClinicalCaresQuery : IRequest<List<ClinicalCareDetailsDto>>
    {

    }
}