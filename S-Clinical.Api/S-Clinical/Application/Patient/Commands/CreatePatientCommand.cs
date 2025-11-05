using MediatR;
using S_Clinical.Domain.Enum;

public class CreatePatientCommand : IRequest<int>
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public GenderTypeEnum Gender { get; set; }
}