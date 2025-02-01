using HotelCard.Core.Enums;

namespace HotelCard.Domain.Entities;

public class Employee : Entity
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool PasswordTemple { get; set; }
    public string? TokenResetPassword { get; set; }
    public DateTime? TokenResetExpiresAt { get; set; }
    public ERole Role { get; set; }
}