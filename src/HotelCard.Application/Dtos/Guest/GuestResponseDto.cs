namespace HotelCard.Application.Dtos.Guest;

public class GuestResponseDto
{
    public string Name { get; set; } = null!;
    public bool? IsHolder { get; set; }
    public string Email { get; set; } = null!;
    public string CellPhone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
}