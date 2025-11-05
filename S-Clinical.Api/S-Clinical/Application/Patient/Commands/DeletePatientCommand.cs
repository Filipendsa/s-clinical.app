using MediatR;

public class DeletePatientCommand : IRequest<Unit>
{
    public int Id { get; set; }
}