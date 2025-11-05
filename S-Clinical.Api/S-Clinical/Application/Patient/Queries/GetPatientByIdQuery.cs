using MediatR;
using System.Collections.Generic;

namespace S_Clinical.Application.Patients.Queries
{
    public class GetPatientByIdQuery : IRequest<PatientDto>
    {
        public int Id { get; set; }
    }
}