namespace HotelCard.Domain.Entities;

public class AccessArea : Entity
{
    public string Name { get; set; } = null!;
    public string PhotoUrl { get; set; } = null!;
    public IList<GuestAccessArea> GuestAccessAreas { get; set; } = new List<GuestAccessArea>();
}