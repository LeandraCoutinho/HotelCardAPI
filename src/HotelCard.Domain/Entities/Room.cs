namespace HotelCard.Domain.Entities;

public class Room : Entity
{
    public string Name { get; set; } = null!;
    public bool IsReserved { get; set; }
    public double DailyPrice { get; set; }
    public ICollection<ContractRoom> ContractRooms { get; set; } = new List<ContractRoom>();
}