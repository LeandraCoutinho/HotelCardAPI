namespace HotelCard.Domain.Entities;

public class GuestAccessArea
{
    public int GuestId { get; set; }
    public Guest Guest { get; set; }
    public int AccessAreaId { get; set; }
    public AccessArea AccessArea { get; set; }
}