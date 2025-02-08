namespace HotelCard.Application.Dtos.Guest;

public class AddDependentDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool IsHolder { get; set; } = false;
    public ulong Cpf { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? PhotoUrl { get; set; }
    public int Room { get; set; }
    public List<int> AccessAreaIds { get; set; } = new List<int>();
}