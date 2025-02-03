namespace HotelCard.Application.Dtos.Auth;

public class ResetPasswordDto
{
    public string Token { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
}