using HotelCard.Application.Dtos.Auth;

namespace HotelCard.Application.Contracts;

public interface IAuthService
{
    Task<TokenDto?> Login(LoginDto loginDto);
    Task<bool> ForgotPassword(string? email);
    Task<bool> ResetPassword(ResetPasswordDto resetPasswordDto);
}