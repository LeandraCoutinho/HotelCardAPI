using HotelCard.Application.Dtos.Guest;
using HotelCard.Core.Enums;

namespace HotelCard.Application.Dtos.Contract;

public class ContractDto
{
    public DateTime BeginAt { get; set; }
    public DateTime FinishAt { get; set; }
    public EPayment PaymentId { get; set; }
    public AddHolderDto Holder { get; set; }
    public List<AddDependentDto> Dependents { get; set; } = new List<AddDependentDto>();
}
