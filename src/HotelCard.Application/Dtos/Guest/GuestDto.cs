namespace HotelCard.Application.Dtos.Guest;

public class GuestDto
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
    public bool IsActive { get; set; }
    public IList<AccessAreasDto> AccessAreas { get; set; }
}