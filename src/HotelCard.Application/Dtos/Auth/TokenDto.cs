namespace HotelCard.Application.Dtos.Auth;

public class TokenDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
}