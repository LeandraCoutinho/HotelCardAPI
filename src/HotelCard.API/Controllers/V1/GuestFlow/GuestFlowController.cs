using HotelCard.Application.Contracts;
using HotelCard.Application.Dtos.Contract;
using HotelCard.Application.Dtos.GuestFlow;
using HotelCard.Application.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelCard.API.Controllers.V1.GuestFlow;

[Route("[controller]")]
public class GuestFlowController : BaseController
{
    private readonly IGuestFlowService _guestFlowService;

    public GuestFlowController(INotificator notificator, IGuestFlowService guestFlowService) : base(notificator)
    {
        _guestFlowService = guestFlowService;
    }
    
    [AllowAnonymous]
    [HttpPost("register-flow")]
    [ProducesResponseType(typeof(GuestFlowResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Create([FromBody] GuestFlowDto dto)
    {
        return CustomResponse(await _guestFlowService.AddFlow(dto));
    }
    
    [Authorize]
    [HttpGet("get-guest")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGuest()
    {
        return CustomResponse(await _guestFlowService.GetFlows());
    }
    
    [HttpGet("get-guest-by-cpf")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGuestByCpf(ulong cpf)
    {
        return CustomResponse(await _guestFlowService.GetFlowsByCpf(cpf));
    }
}