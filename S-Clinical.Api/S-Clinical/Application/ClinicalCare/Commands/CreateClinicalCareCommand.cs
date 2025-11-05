using MediatR;


public class CreateClinicalCareCommand : IRequest<int>
{
    public int PatientId { get; set; }
    public int SequentialNumber { get; set; }
}