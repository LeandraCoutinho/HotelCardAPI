namespace HotelCard.Application.Dtos.Guest;

public class UpdateGuestDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string CellPhone { get; set; } = null!;
    public ulong Cpf { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? PhotoUrl { get; set; }
    public List<int> AccessAreaIds { get; set; } = new List<int>();
}