using HotelCard.Core.Enums;

namespace HotelCard.Domain.Entities;

public class Consumption
{
    public int Id { get; set; }
    public DateTime DateConsumption { get; set; }
    public EPayment PaymentId { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int GuestId { get; set; }
    public Guest Guest { get; set; }
    public IList<ConsumptionProduct> ConsumptionProducts { get; set; } = new List<ConsumptionProduct>();
}