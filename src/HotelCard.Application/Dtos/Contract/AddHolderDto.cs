namespace HotelCard.Application.Dtos.Contract;

public class AddHolderDto
{
    public string Name { get; set; } = null!;
    public bool IsHolder { get; set; }
    public string Email { get; set; } = null!;
    public ulong Cpf { get; set; }
    public string CellPhone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public string? PhotoUrl { get; set; }
    public int Room { get; set; }
    public List<int> AccessAreaIds { get; set; } = new List<int>();
}