using HotelCard.Application.Contracts;
using HotelCard.Application.Dtos.Consumption;
using HotelCard.Application.Dtos.Contract;
using HotelCard.Application.Notification;
using HotelCard.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelCard.API.Controllers.V1.Consumption;

[AllowAnonymous]
[Route("[controller]")]
public class ConsumptionController : BaseController
{
    private readonly IConsumptionService _consumptionService;
    
    public ConsumptionController(INotificator notificator, IConsumptionService consumptionService) : base(notificator)
    {
        _consumptionService = consumptionService;
    }
    
    [Authorize(Roles = nameof(ERole.Operator))]
    [HttpPost("create-consumption")]
    [ProducesResponseType(typeof(ConsumptionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Create([FromBody] AddConsumptionDto dto)
    {
        return CustomResponse(await _consumptionService.Add(dto));
    }
    
    [Authorize]
    [HttpGet("get-consumption-by-guest")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGuest([FromQuery] ulong cpf)
    {
        return CustomResponse(await _consumptionService.GetConsumptionByGuest(cpf));
    }
    
    [Authorize]
    [HttpGet("get-consumption-by-card")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGuestByCard([FromQuery] ulong card)
    {
        return CustomResponse(await _consumptionService.GetConsumptionByCard(card));
    }
    
    [Authorize]
    [HttpGet("get-all")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return CustomResponse(await _consumptionService.GetAll());
    }
}