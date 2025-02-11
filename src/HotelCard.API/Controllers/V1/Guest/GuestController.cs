using HotelCard.Application.Contracts;
using HotelCard.Application.Dtos.Guest;
using HotelCard.Application.Notification;
using HotelCard.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelCard.API.Controllers.V1.Guest;

[Route("[controller]")]
public class GuestController : BaseController
{
    private readonly IGuestService _guestService;
    
    public GuestController(INotificator notificator, IGuestService guestService) : base(notificator)
    {
        _guestService = guestService;
    }
    
    [Authorize]
    [HttpGet("get-guest-by-card")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGuestByCard([FromQuery] ulong cardOfNumber)
    {
        return CustomResponse(await _guestService.GetGuestByCard(cardOfNumber));
    }
    
    [Authorize]
    [HttpGet("get-guest-by-email")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGuestByEmail(string email)
    {
        return CustomResponse(await _guestService.GetGuestByEmail(email));
    }
    
    [Authorize]
    [HttpGet("get-all")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return CustomResponse(await _guestService.GetAll());
    }
    
    [Authorize]
    [HttpPut("update-guest")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateGuest(int id, [FromBody] UpdateGuestDto dto)
    {
        return CustomResponse(await _guestService.UpdateGuest(id, dto));
    }
    
    [Authorize(Roles = nameof(ERole.Administrator))]
    [HttpPatch("disable-guest")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DisableGuest(ulong cardOfNumber)
    {
        return CustomResponse(await _guestService.DisableGuest(cardOfNumber));
    }
}