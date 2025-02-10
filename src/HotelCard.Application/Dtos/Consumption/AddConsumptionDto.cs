using HotelCard.Core.Enums;

namespace HotelCard.Application.Dtos.Consumption;

public class AddConsumptionDto
{
    public ulong CardNumber { get; set; }
    public List<ProductConsumptionDto> Products { get; set; } = new();
    public EPayment Payment { get; set; }
}