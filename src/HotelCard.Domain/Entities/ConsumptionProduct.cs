namespace HotelCard.Domain.Entities;

public class ConsumptionProduct
{
    public int ConsumptionId { get; set; }
    public Consumption Consumption { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
}