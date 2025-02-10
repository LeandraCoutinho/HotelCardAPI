using HotelCard.Application.Contracts;
using HotelCard.Application.Dtos.Card;
using HotelCard.Application.Dtos.Guest;
using HotelCard.Application.Notification;
using HotelCard.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelCard.API.Controllers.V1.Card;

[Microsoft.AspNetCore.Components.Route("[controller]")]
public class CardController : BaseController
{
    private readonly ICardService _cardService;
    
    public CardController(INotificator notificator, ICardService cardService) : base(notificator)
    {
        _cardService = cardService;
    }
    
    [Authorize(Roles = nameof(ERole.Administrator))]
    [HttpPost("register-card")]
    [ProducesResponseType(typeof(GuestDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> RegisterCard([FromBody] RegisterCardDto dto)
    {
        return CustomResponse(await _cardService.RegisterCard(dto));
    }
    
    [Authorize(Roles = nameof(ERole.Administrator))]
    [HttpPatch("reset-card")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> ResetCard()
    {
        var success = await _cardService.ResetCard();

        if (!success)
        {
            return BadRequest("Não foi possível redefinir os cartões.");
        }

        return NoContent();    
    }
}