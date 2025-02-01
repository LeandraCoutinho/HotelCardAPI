namespace HotelCard.Domain.Entities;

public class Guest : Entity
{
    public string Name { get; set; } = null!;
    public bool IsHolder { get; set; }
    public string Email { get; set; } = null!;
    public ulong Cpf { get; set; }
    public string CellPhone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public ulong? CardOfNumber { get; set; }
    public string? PhotoUrl { get; set; }
    public IList<Consumption> Consumptions { get; set; } = new List<Consumption>();
    public IList<GuestAccessArea> GuestAccessAreas { get; set; } = new List<GuestAccessArea>();
}