using HotelCard.Core.Enums;

namespace HotelCard.Application.Dtos.Employee;

public class EmployeeDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool PasswordTemple { get; set; }
    public DateTime? TokenResetExpiresAt { get; set; }
    public ERole Role { get; set; }
}