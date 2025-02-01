namespace HotelCard.Domain.Entities;

public class Product : Entity
{
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public IList<ConsumptionProduct> ConsumptionProducts { get; set; } = new List<ConsumptionProduct>();
}