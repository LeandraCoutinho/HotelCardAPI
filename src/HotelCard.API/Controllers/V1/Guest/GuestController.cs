using HotelCard.Application.Contracts;
using HotelCard.Application.Notification;
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
    [HttpGet("get-guest")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGuest([FromQuery] ulong cardOfNumber)
    {
        return CustomResponse(await _guestService.GetGuest(cardOfNumber));
    }
}