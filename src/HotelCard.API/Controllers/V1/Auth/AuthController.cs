using HotelCard.Application.Contracts;
using HotelCard.Application.Dtos.Auth;
using HotelCard.Application.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelCard.API.Controllers.V1.Auth;

[AllowAnonymous]
[Route("[controller]")]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;


    public AuthController(INotificator notificator, IAuthService authService) : base(notificator)
    {
        _authService = authService;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var token = await _authService.Login(dto);
        return token != null ? Ok(token) : Unauthorized(new[] { "Email e/ou senha incorretos." });
    }
    
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromQuery] string? email)
    {
        var success = await _authService.ForgotPassword(email);
    
        if (!success)
            return CustomResponse(false); 

        return Ok("Um e-mail foi enviado com instruções para redefinir sua senha.");
    }
    
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDto dto)
    {
        var success = await _authService.ResetPassword(dto);
    
        if (!success)
            return CustomResponse(false); 

        return Ok("Senha redefinida com sucesso!");    
    }
}