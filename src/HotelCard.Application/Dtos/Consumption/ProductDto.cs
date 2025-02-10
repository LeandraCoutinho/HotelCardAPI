namespace HotelCard.Application.Dtos.Consumption;

public class ProductDto
{
    public int ProductId { get; set; }
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public int Quantity { get; set; }
    public double? TotalValue { get; set; }
}