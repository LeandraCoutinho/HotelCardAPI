namespace HotelCard.Domain.Entities;

public class GuestFlow
{
    public int Id { get; set; }
    public int GuestId { get; set; }
    public Guest Guest { get; set; }
    public int AccessAreaId { get; set; } 
    public AccessArea AccessArea { get; set; } = null!;
    public DateTime AccessTime { get; set; }
}