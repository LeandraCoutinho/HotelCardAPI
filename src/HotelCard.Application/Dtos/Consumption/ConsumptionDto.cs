namespace HotelCard.Application.Dtos.Consumption;

public class ConsumptionDto
{
    public int Id { get; set; }
    public string GuestName { get; set; } = null!;
    public DateTime DateConsumption { get; set; }
    public List<ProductDto> Products { get; set; } = new();
}